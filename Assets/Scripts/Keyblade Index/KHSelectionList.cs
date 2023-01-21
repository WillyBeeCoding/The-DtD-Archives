using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using TMPro;

public class KHSelectionList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // All private variables
    private List<string> allGames;
    private float incrementVal;
    private int gameIndex;
    private bool hoverOn;
    private bool switchingLists;
    private Scrollbar scrollbar; 
    private ScrollRect scrollrect;

    // Public objects/scripts in hierarchy 
    public List<GameObject> itemsList;
    public List<RectTransform> thingsToColorShift;
    public GameObject listContentObj;
    public Transform listParentTransform;
    public RectTransform particleBackground;
    public RectTransform emblemBackground;
    public MenuHighlighter highlight;
    public GameIconsList iconList;

    // Public prefab variables
    public GameObject listItemPrefab;

    // Public asset components (images, audio, text, etc.)
    public Image keybladeImage;
    public Image statsImage;
    public TextMeshProUGUI keybladeDesc;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;

    void Start() {
        // Setting our private variables
        allGames = new List<string> {"KH", "KH2", "KHDays", "KHBBS", "KHCoded", "KHDDD", "KHX", "KH3"};
        incrementVal = 58f;
        gameIndex = allGames.IndexOf(MasterDatabase.GetCurrentKeyblade().keybladeGame);
        switchingLists = true;
        scrollbar = GetComponentInChildren<Scrollbar>();
        scrollrect = GetComponentInChildren<ScrollRect>();
        
        // Initializing the list and color of the screen.
        refreshList(allGames[gameIndex], MasterDatabase.GetCurrentKeyblade().keybladeName);
        changeColor(gameIndex);
    }

    // Update is called once per frame
    void Update() {
        if (hoverOn && scrollbar.enabled) {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && scrollbar.value < 0.999f) {
                mouseScroll(-1);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f && scrollbar.value > 0.001f) {
                mouseScroll(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            shift(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            shift(1);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) { hoverOn = true; }
    public void OnPointerExit(PointerEventData eventData) { hoverOn = false; }

    public void shift(int direction) {
        if ((gameIndex > 0 && direction == -1) || 
            (gameIndex < allGames.Count-1 && direction == 1)) {
            gameIndex += direction;
            refreshList(allGames[gameIndex], MasterDatabase.GetCurrentKeyblade().keybladeName);
            iconList.Shift(gameIndex);
            changeColor(gameIndex);
            StartCoroutine(listSlideAnim());
        } else {
            audioSource.PlayOneShot(audioClips[3], 0.25f); 
        }
    }

    public void jumpTo(int index) {
        if (gameIndex == index) {
            audioSource.PlayOneShot(audioClips[3], 0.25f);
        } else {
            gameIndex = index;
            refreshList(allGames[gameIndex], MasterDatabase.GetCurrentKeyblade().keybladeName);
            iconList.Shift(gameIndex);
            changeColor(gameIndex);
            StartCoroutine(listSlideAnim());
        }
    }
        

    /// <summary>
    /// This method moves the scrollbar as needed when the mouse scrollwheel is used.
    /// </summary>
    private void mouseScroll(int direction) {
        scrollbar.value -= direction*(1f/(scrollbar.numberOfSteps - 1));
        highlight.MoveVert(direction*incrementVal, 0f);
        audioSource.PlayOneShot(audioClips[1], 0.25f);
    }

    /// <summary>
    /// This method runs every time a new set of items needs to appear in the list.
    /// </summary>
    private void refreshList(string gameName, string currentKeyblade) {

        // Removes the current list
        foreach (Transform child in listParentTransform) { GameObject.Destroy(child.gameObject); }
        listParentTransform.DetachChildren();

        Button currentButton = null; // Variable used to determine what button to click next.
        switchingLists = true; // Bool value for the sound effect in the button listener.
        iconList.Shift(gameIndex); // Shift the game icon from old game to new.

        // This part loads in all the keyblades from the specified game
        foreach (Keyblade keyblade in MasterDatabase.GetKeybladesByGame(gameName)) {

            GameObject newKeybladeItem = Instantiate(listItemPrefab, listParentTransform);
            newKeybladeItem.name = keyblade.keybladeName + " List Item";
            newKeybladeItem.GetComponent<TextMeshProUGUI>().text = keyblade.keybladeName;

            // Accesses the button component and assignes a bunch of listeners to it.
            Button keybladeButton = newKeybladeItem.GetComponent<Button>();
            keybladeButton.onClick.AddListener(delegate { 
                highlight.NewPosition(newKeybladeItem); // Moves highlight to this button
                refreshUI(keyblade); // Updates display info with this button's keyblade info
                MasterDatabase.SetCurrentKeyblade(keyblade); // Sets current keyblade as this
                iconList.RefreshIcons(keyblade, allGames); // Updates the opacity of game icons

                // Determines what sound happens upon clicking
                if (switchingLists) { audioSource.PlayOneShot(audioClips[2], 0.25f); } 
                else { audioSource.PlayOneShot(audioClips[0], 0.25f); }
            });

            // Figures out what button should be selected once the list is shifted. If there's a keyblade with a
            // matching name in the new list, it'll go to that one. Otherwise, it defaults to the first keyblade.
            if (currentButton == null || keyblade.keybladeName == currentKeyblade) currentButton = keybladeButton;
        }

        // This part reloads all the new items under the view panel into the itemsList variable.
        itemsList = new List<GameObject>();
        foreach (Transform child in listParentTransform) { itemsList.Add(child.gameObject); }

        // This part checks to see if the list is big enough to require a scroll bar, and enables/disables it as needed.
        scrollbar.numberOfSteps = itemsList.Count - 12;
        if (scrollbar.numberOfSteps < 2) {
            scrollbar.GetComponent<CanvasGroup>().alpha = 0;
            scrollbar.enabled = false;
        } else {
            scrollbar.GetComponent<CanvasGroup>().alpha = 1;
            scrollbar.enabled = true;
        }

        // And finally, we end with pushing the button we want on the new page after everything is loaded.
        StartCoroutine(pushButton(currentButton));
    }

    /// <summary>
    /// This method updates all the content on the Keyblade Index screen, such as the images, video, and text.
    /// </summary>
    private void refreshUI(Keyblade keyblade) {
        keybladeImage.sprite = keyblade.image;
        keybladeDesc.text = keyblade.gameQuote + "\n\n" + keyblade.desc;
        if (keyblade.footage) {
            videoPlayer.clip = keyblade.footage;
        }
        if (keyblade.stats) {
            statsImage.sprite = keyblade.stats;
        }
    }

    /// <summary>
    /// This method changes the hue of elements on screen to match the game selected.
    /// </summary>
    private void changeColor(int gameIndex) {
        Color newColor = new Color32(255, 255, 255, 255);
        switch(gameIndex) {
            case 0:
                Debug.Log("Swapping to KH");
                newColor = new Color32(0, 204, 255, 255);
                break;
            case 1:
                Debug.Log("Swapping to KH2");
                newColor = new Color32(119, 149, 236, 255);
                break;
            case 2:
                Debug.Log("Swapping to Days");
                newColor = new Color32(255, 129, 61, 255);
                break;
            case 3:
                Debug.Log("Swapping to BBS");
                newColor = new Color32(176, 232, 247, 255);
                break;
            case 4:
                Debug.Log("Swapping to Coded");
                newColor = new Color32(239, 217, 111, 255);
                break;
            case 5:
                Debug.Log("Swapping to DDD");
                newColor = new Color32(255, 153, 187, 255);
                break;
            case 6:
                Debug.Log("Swapping to Union X");
                newColor = new Color32(126, 218, 184, 255);
                break;
            case 7:
                Debug.Log("Swapping to KH3");
                newColor = new Color32(163, 153, 190, 255);
                break;
            default:
                break;
        }

        foreach (RectTransform obj in thingsToColorShift) LeanTween.color(obj, newColor, 0.2f).setEaseInQuad();
        newColor.a = 0.5f;
        LeanTween.color(particleBackground, newColor, 0.2f).setEaseInQuad();
        LeanTween.color(emblemBackground, newColor, 0.2f).setEaseInQuad();
    }

    /// <summary>
    /// This method presses the proper Keyblade button when moving to a new list of Keyblades.
    /// </summary>
    private IEnumerator pushButton(Button button) {
        yield return new WaitForSeconds(0.1f); // Waits a bit for the list to load up
        scrollbar.value = 1f; // Sets the scrollbar back up to the top

        // Math for how many scroll steps down it takes for the button we want to be visible in the list, and if there are indeed some steps down required then it'll move the scrollbar value down that much.
        float stepsAway = -(button.transform.localPosition.y + 12*incrementVal + 24f)/incrementVal; 
        if (stepsAway >= 1 && scrollbar.IsActive()) scrollbar.value -= (stepsAway/(scrollbar.numberOfSteps - 1));

        // Pushes the button, and then resets the switchingLists value back to false.
        button.onClick.Invoke();
        switchingLists = false;
    }

    /// <summary>
    /// This method performs a LeanTween animation on the list while it's shifting.
    /// </summary>
    private IEnumerator listSlideAnim() {
        yield return new WaitForEndOfFrame();
        LeanTween.moveLocalX(listContentObj, -30f, 0.1f);
        LeanTween.alphaCanvas(listContentObj.GetComponent<CanvasGroup>(), 0f, 0.1f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalX(listContentObj, 0f, 0.1f);
        LeanTween.alphaCanvas(listContentObj.GetComponent<CanvasGroup>(), 1f, 0.1f);
    }
}

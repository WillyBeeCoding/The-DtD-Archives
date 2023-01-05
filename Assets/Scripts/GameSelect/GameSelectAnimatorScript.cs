using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSelectAnimatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject boxArt;
    public GameObject gameLogo;
    public GameObject blackScreen;
    public int gameIndex = 0;

    void Start()
    {
        LTSeq sequence = LeanTween.sequence();
        sequence.append(LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 0f, 2f).setEaseOutSine());
        sequence.append(() => {blackScreen.SetActive(false);});
        
        selectButton.GetComponent<Button>().onClick.AddListener(GameSelected);
        leftButton.GetComponent<Button>().onClick.AddListener(PrevGame);
        rightButton.GetComponent<Button>().onClick.AddListener(NextGame);
    }

    public void GameSelected()
    {
        selectButton.GetComponent<SelectButtonAnimatorScript>().selected = true;
        selectButton.GetComponent<SelectButtonAnimatorScript>().FadeAway();
        leftButton.GetComponent<LRButtonAnimatorScript>().FadeAway();
        rightButton.GetComponent<LRButtonAnimatorScript>().FadeAway();
        //boxArt.GetComponent<BoxArtAnimatorScript>().FadeAway();
        gameLogo.GetComponent<LogoAnimatorScript>().ShrinkUp();

        blackScreen.SetActive(true);

        LTSeq sequence = LeanTween.sequence();
        sequence.append(3f);
        sequence.append(LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 1f, 3f).setEaseInQuad());
    }

    public void PrevGame()
    {
        selectButton.GetComponent<SelectButtonAnimatorScript>().PrevColor();
        leftButton.GetComponent<LRButtonAnimatorScript>().PrevColor();
        rightButton.GetComponent<LRButtonAnimatorScript>().PrevColor();
        boxArt.GetComponent<BoxArtAnimatorScript>().PrevArt();
        gameLogo.GetComponent<LogoAnimatorScript>().PrevLogo();
        if (gameIndex > 0) { gameIndex--; }
    }

    public void NextGame()
    {
        selectButton.GetComponent<SelectButtonAnimatorScript>().NextColor();
        leftButton.GetComponent<LRButtonAnimatorScript>().NextColor();
        rightButton.GetComponent<LRButtonAnimatorScript>().NextColor();
        boxArt.GetComponent<BoxArtAnimatorScript>().NextArt();
        gameLogo.GetComponent<LogoAnimatorScript>().NextLogo();
        if (gameIndex < 11) { gameIndex++; }
    }




}

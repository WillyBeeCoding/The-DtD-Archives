using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameIconsList : MonoBehaviour
{
    public List<GameObject> gameIcons;
    public List<GameObject> gameTitles;
    public GameObject prevGame;
    public GameObject nextGame;
    private int index;
    
    void Start() {
        index = 0;
        Shift(index);
    }

    public void Shift(int newIndex) {
        LeanTween.cancel(gameIcons[index]); // Cancel the prev icon spin.

        // Puts the icon back in it's idle position.
        LeanTween.scaleX(gameIcons[index], 0.1f, 0.1f).setEaseInQuad();
        LeanTween.moveLocalY(gameIcons[index], 0f, 0.1f).setEaseOutExpo();

        // Moves the current title text out of the way.
        LeanTween.moveLocalY(gameTitles[index], 20f, 0.2f).setEaseOutExpo();
        LeanTween.alphaCanvas(gameTitles[index].GetComponent<CanvasGroup>(), 0f, 0.2f).setEaseOutExpo();

        index = newIndex; // Update the index with the new value.

        // Sets the new icon into an elevated spin.
        LeanTween.scaleX(gameIcons[index], 0f, 1f).setEaseInQuad().setLoopPingPong();
        LeanTween.moveLocalY(gameIcons[index], 5f, 1f).setEaseOutExpo();

        // Moves the new title into view.
        LeanTween.moveLocalY(gameTitles[index], 10f, 0.2f).setEaseOutExpo();
        LeanTween.alphaCanvas(gameTitles[index].GetComponent<CanvasGroup>(), 1f, 0.2f).setEaseOutExpo();
    }
    
    public void RefreshIcons(Keyblade keyblade, List<string> allGames) {
        List<string> games = MasterDatabase.GetGamesWithKeybladeName(keyblade.keybladeName);
        for (int i = 0; i < allGames.Count; i++) {
            if (games.Contains(allGames[i])) { 
                LeanTween.alpha(gameIcons[i].GetComponent<Image>().GetComponent<RectTransform>(), 1f, 0f);
            } else { 
                LeanTween.alpha(gameIcons[i].GetComponent<Image>().GetComponent<RectTransform>(), 0.25f, 0f);
            }
        }
    }
}

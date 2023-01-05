using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxArtAnimatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> boxarts = new List<GameObject>();
    public int imageIndex = 0;

    public void PrevArt()
    {
        if (imageIndex > 0)
        {
            LeanTween.moveLocal(boxarts[imageIndex], new Vector2(165, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(boxarts[imageIndex--].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            Debug.Log("Image Index is now " + imageIndex);
            LeanTween.moveLocal(boxarts[imageIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(boxarts[imageIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();
        }
    }

    public void NextArt()
    {
        if (imageIndex + 1 < boxarts.Count)
        {
            LeanTween.moveLocal(boxarts[imageIndex], new Vector2(165, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(boxarts[imageIndex++].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            Debug.Log("Image Index is now " + imageIndex);
            LeanTween.moveLocal(boxarts[imageIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(boxarts[imageIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();
        }
    }


    public void FadeAway()
    {
        LeanTween.moveLocal(boxarts[imageIndex], new Vector2(165, 0), 2f);
        LeanTween.alpha(boxarts[imageIndex++].GetComponent<RectTransform>(), 0f, 2f);
    }
}

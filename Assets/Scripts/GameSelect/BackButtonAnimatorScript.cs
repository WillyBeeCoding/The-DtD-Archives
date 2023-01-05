using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackButtonAnimatorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public GameObject hoverImage;
    public GameObject pressedImage;
    public GameObject blackScreen;

    public void RevertScene() 
    {
        pressedImage.SetActive(true);
        LeanTween.alpha(pressedImage.GetComponent<RectTransform>(), 0f, 0.5f).setEaseOutSine();
        blackScreen.SetActive(true);
        LeanTween.alpha(blackScreen.GetComponent<RectTransform>(), 1f, 1f).setEaseInQuad();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.alpha(hoverImage.GetComponent<RectTransform>(), 1f, 0.3f).setEaseOutQuad();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.alpha(hoverImage.GetComponent<RectTransform>(), 0f, 0.3f).setEaseOutQuad();
    }
}

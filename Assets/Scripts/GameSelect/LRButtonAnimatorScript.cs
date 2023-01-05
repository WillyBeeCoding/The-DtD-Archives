using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LRButtonAnimatorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> auras = new List<GameObject>();
    public int buttonDirection;
    public int colorIndex = 0;

    public void PrevColor()
    {
        if (colorIndex > 0)
        {
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(20, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex--].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            //Index Change
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();
            if (buttonDirection < 0) { LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 1f, 0.15f); }
        }
    }

    public void NextColor()
    {
        if (colorIndex + 1 < buttons.Count)
        {
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(-20, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex++].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            //Index Change
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();
            if (buttonDirection > 0) { LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 1f, 0.15f); }
        }
    }

    public void FadeAway()
    {
        LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 0f, 0.7f);
        LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.7f);
        LeanTween.moveLocal(buttons[colorIndex], new Vector2(buttonDirection * 40, 0), 1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.moveLocal(this.gameObject, new Vector2(buttonDirection * 455, 0), 0.3f).setEaseOutQuad();
        LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 1f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.moveLocal(this.gameObject, new Vector2(buttonDirection * 450, 0), 0.3f).setEaseOutQuad();
        LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
    }
}

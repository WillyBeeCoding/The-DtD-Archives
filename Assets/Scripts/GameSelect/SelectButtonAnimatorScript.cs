using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectButtonAnimatorScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public List<GameObject> buttons = new List<GameObject>();
    public List<GameObject> auras = new List<GameObject>();
    public int colorIndex = 0;
    public bool selected = false;

    public void PrevColor()
    {
        if (colorIndex > 0)
        {
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(20, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex--].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            Debug.Log("Image Index is now " + colorIndex);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();
        }
    }

    public void NextColor()
    {
        if (colorIndex + 1 < buttons.Count)
        {
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(-20, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex++].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            Debug.Log("Image Index is now " + colorIndex);
            LeanTween.moveLocal(buttons[colorIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();

        }
    }

    public void FadeAway()
    {
        LeanTween.alpha(buttons[colorIndex].GetComponent<RectTransform>(), 0f, 0.7f);
        LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.5f).setEaseOutQuad();
        LeanTween.scale(buttons[colorIndex], new Vector2(1.2f, 1.2f), 0.7f);
        LeanTween.scale(auras[colorIndex], new Vector2(1.3f, 1.3f), 0.5f).setEaseOutQuad();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
        {
            LeanTween.moveLocal(this.gameObject, new Vector2(0, 10), 0.3f).setEaseOutQuad();
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 1f, 0.15f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            LeanTween.moveLocal(this.gameObject, new Vector2(0, 0), 0.3f).setEaseOutQuad();
            LeanTween.alpha(auras[colorIndex].GetComponent<RectTransform>(), 0f, 0.15f);
        }

    }
}

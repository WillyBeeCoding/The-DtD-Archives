using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimatorScript : MonoBehaviour
{

    public List<GameObject> logos = new List<GameObject>();
    public int imageIndex = 0;

    void Start()
    {
        LogoHover();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            PrevLogo();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextLogo();
        }
    }

    public void LogoHover()
    {
        LeanTween.moveLocal(this.gameObject, new Vector2(this.gameObject.transform.localPosition.x, this.gameObject.transform.localPosition.y + 10f), 1f).setEaseInOutQuad().setLoopPingPong();
    }

    public void PrevLogo()
    {
        if (imageIndex > 0)
        {
            LeanTween.moveLocal(logos[imageIndex], new Vector2(100, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(logos[imageIndex--].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            LeanTween.moveLocal(logos[imageIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(logos[imageIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();

        }
    }

    public void NextLogo()
    {
        if (imageIndex + 1 < logos.Count)
        {
            LeanTween.moveLocal(logos[imageIndex], new Vector2(-100, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(logos[imageIndex++].GetComponent<RectTransform>(), 0f, 1f).setEaseOutExpo();
            LeanTween.moveLocal(logos[imageIndex], new Vector2(0, 0), 1f).setEaseOutExpo();
            LeanTween.alpha(logos[imageIndex].GetComponent<RectTransform>(), 1f, 1f).setEaseOutExpo();

        }
    }

    public void ShrinkUp()
    {
        LeanTween.moveLocal(logos[imageIndex], new Vector2(0f, 175f), 1f).setEaseOutQuad();
        LeanTween.scale(logos[imageIndex], new Vector2(0.75f, 0.75f), 1f).setEaseOutQuad();
    }

    public void SpinOutro()
    {

        LeanTween.scale(logos[imageIndex], new Vector2(0f, 1f), 1f).setEaseInQuad().setLoopPingPong();
        LeanTween.moveLocal(this.gameObject, new Vector2(0f, 100f), 6f).setEaseInOutQuad();
        LeanTween.scale(this.gameObject, new Vector2(0.7f, 0.7f), 6f).setEaseOutQuad();
    }
}

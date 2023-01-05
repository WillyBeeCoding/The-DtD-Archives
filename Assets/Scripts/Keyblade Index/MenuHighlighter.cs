using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuHighlighter : MonoBehaviour
{
    public float incrementVal;
    private bool inFrame;

    public GameObject lightParent;
    public GameObject lightBall;

    void Start()
    {
        LightHover();
        incrementVal = 58f;
        inFrame = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y > 360f || transform.localPosition.y < -360f ) {
            inFrame = false;
            LeanTween.alpha(GetComponent<Image>().GetComponent<RectTransform>(), 0f, 0f);
            LeanTween.alpha(lightBall.GetComponent<Image>().GetComponent<RectTransform>(), 0f, 0f);
        } else if (transform.localPosition.y < 360f && transform.localPosition.y > -360f) {
            inFrame = true;
            LeanTween.alpha(GetComponent<Image>().GetComponent<RectTransform>(), 1f, 0f);
            LeanTween.alpha(lightBall.GetComponent<Image>().GetComponent<RectTransform>(), 1f, 0f);
        }
    }

    public void NewPosition(GameObject itemLocation) {
        Debug.Log("New position wooooo at " + itemLocation.transform.localPosition.y);
        float newY = itemLocation.transform.position.y - 3f;
        float speed = inFrame ? 0.1f : 0f;
        LeanTween.moveY(this.gameObject, newY, speed).setEaseOutQuad();
        LeanTween.moveY(lightParent, newY, speed*3f).setEaseInOutQuad();
    }

    public void LightHover() {
        LeanTween.moveLocalX(lightBall, lightBall.transform.localPosition.x + 25f, 0.75f).setEaseInOutQuad().setLoopPingPong();
        LeanTween.moveLocalY(lightBall, lightBall.transform.localPosition.y + 40f, 1f).setEaseInOutQuad().setLoopPingPong();
    }

    public void NewIncrement(float newInc) {
        incrementVal = newInc;
    }

    public void MoveVert(float direction, float time) {
        LeanTween.moveLocalY(this.gameObject, this.transform.localPosition.y + direction, time).setEaseOutQuad();
        LeanTween.moveLocalY(lightParent, lightParent.transform.localPosition.y + direction, time).setEaseOutQuad();
    }
}

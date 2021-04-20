using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{
    public CanvasGroup uiElement;
    private float timer = 3f;

    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                FadeOut();
        }
    }

    public void PopIn()
    {
        uiElement.alpha = 1;
        timer = 3f;

    }

     public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup canvas, float start, float end)
    {
        float lerpTime = .5f;
        float timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;
            float currentValue = Mathf.Lerp(start, end, percentageComplete);
            canvas.alpha = currentValue;
            if (percentageComplete >= 1)
                break;
            yield return new WaitForEndOfFrame();
        }
    }
}

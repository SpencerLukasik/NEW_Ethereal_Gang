using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour
{
    public Rikayon player;
    public CanvasGroup reminder;
    private bool firstKill;

    void Start()
    {
        firstKill = true;
    }
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Bean")
        {
            player.bodies.Add(hit.gameObject);
            if (firstKill)
            {
                firstKill = false;
                reminder.gameObject.SetActive(true);
                StartCoroutine(ActivateReminder(3f));
            }
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (player.bodies.Contains(hit.gameObject))
            player.bodies.Remove(hit.gameObject);
    }

    public IEnumerator ActivateReminder(float time)
    {
        while (true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                StartCoroutine(FadeOut());
                break;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FadeOut()
    {
        while (true)
        {
            reminder.alpha -= .01f;
            if (reminder.alpha <= 0)
            {
                reminder.gameObject.SetActive(false);
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}

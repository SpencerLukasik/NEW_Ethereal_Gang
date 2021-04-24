using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyManager : MonoBehaviour
{
    public Rikayon player;
    public Text beanCountUINum;
    public RawImage beanImageUI;
    public Text textReminder;
    public Text beansEatenUI;
    public Color invisible;
    private int beanCount;
    private int beansEaten;
    
    private bool firstKill;

    void Start()
    {
        firstKill = true;
        beansEaten = 0;
    }
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Bean")
        {
            if (beanCount <= 0)
                EnableBeanReminder();
            beanCount += 1;
            beanCountUINum.text = "x " + beanCount.ToString();
            player.bodies.Add(hit.gameObject);
            if (firstKill)
            {
                firstKill = false;
                textReminder.gameObject.SetActive(true);
                StartCoroutine(ActivateTextReminder(3f));
            }
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (player.bodies.Contains(hit.gameObject))
        {
            player.bodies.Remove(hit.gameObject);
            beanCount -= 1;
            if (beanCount <= 0)
                DisableBeanReminder();
            else
                beanCountUINum.text = "x " + beanCount.ToString();
        }
    }

    private void EnableBeanReminder()
    {
        beanCountUINum.gameObject.SetActive(true);
        beanImageUI.gameObject.SetActive(true);
    }

    public void DisableBeanReminder()
    {
        beanCount = 0;
        beanCountUINum.gameObject.SetActive(false);
        beanImageUI.gameObject.SetActive(false);
    }

    public void AddToBodyCount(int newBodies)
    {
        beansEaten += newBodies;
        beansEatenUI.text = beansEaten.ToString();
    }

    public IEnumerator ActivateTextReminder(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        while (textReminder.color.a >= 0)
        {
            textReminder.color = Color.Lerp(textReminder.color, invisible, 1f * Time.deltaTime);
            yield return null;
        }

        textReminder.gameObject.SetActive(false);
    }
}

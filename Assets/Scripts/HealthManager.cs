using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public HealthBar UIhealthBar;
    public GameObject fpc;
    public GameObject tpc;
    public GameObject dc;
    public UI_Fade fade;
    private bool alive;
    public Texture healthyPng;
    public Texture damagedPng;
    // Start is called before the first frame update
    void Start()
    {
        alive = true;
        curHealth = 10f;
        maxHealth = 10f;
        UIhealthBar.setMaxHealth(maxHealth, curHealth);
        fade.PopIn();
    }

    public void addHealth()
    {
        //if (alive)
        //    transform.parent.gameObject.GetComponent<PlayerConnection>().ServerAddHealth();
        //if (curHealth > (maxHealth/2) && alive)
        //    transform.GetChild(7).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = healthyPng;
        maxHealth += 3f;
        curHealth += 5f;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        UIhealthBar.setMaxHealth(maxHealth, curHealth);
        fade.PopIn();
    }

    public void takeDamage(float damage)
    {
        //if (alive)
        //{
        //    transform.parent.gameObject.GetComponent<PlayerConnection>().ServerTakeDamage(damage);
        //    if (curHealth <= (maxHealth/2) && alive)
        //    {
        //        transform.GetChild(7).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = damagedPng;
        //    }
        //    if (curHealth <= 0f && alive)
        //    {
        //        transform.parent.gameObject.GetComponent<PlayerConnection>().ServerKillMyUnit();
        //        transform.parent.gameObject.GetComponent<PlayerConnection>().ClientActivateDeathCam();
        //        alive = false;
        //    }
       // }
       if (alive)
       {
            curHealth -= damage;
            UIhealthBar.takeDamage(damage);
            fade.PopIn();

            if (curHealth <= (maxHealth/2) && alive)
            {
                transform.GetChild(7).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = damagedPng;
            }

            if (curHealth <= 0f && alive)
            {
                transform.gameObject.GetComponent<Rikayon>().animator.SetTrigger("Die");
                transform.gameObject.GetComponent<Rikayon>().enabled = false;
                transform.GetChild(4).gameObject.SetActive(false);
                transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(6).gameObject.SetActive(true);
                alive = false;
            }
       }
    }
}

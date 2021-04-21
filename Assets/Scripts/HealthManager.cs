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
        curHealth = 1000f;
        maxHealth = 1000f;
        UIhealthBar.setMaxHealth(maxHealth, curHealth);
        fade.PopIn();
    }

    public void addHealth()
    {
        if (alive)
            transform.parent.gameObject.GetComponent<PlayerConnection>().ServerAddHealth();
        if (curHealth > (maxHealth/2) && alive)
        transform.GetChild(7).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = healthyPng;
    }

    public void takeDamage(float damage)
    {
        if (alive)
        {
            transform.parent.gameObject.GetComponent<PlayerConnection>().ServerTakeDamage(damage);
            if (curHealth <= (maxHealth/2) && alive)
            {
                transform.GetChild(7).GetChild(0).GetChild(2).GetComponent<RawImage>().texture = damagedPng;
            }
            if (curHealth <= 0f && alive)
            {
                transform.parent.gameObject.GetComponent<PlayerConnection>().ServerKillMyUnit();
                transform.parent.gameObject.GetComponent<PlayerConnection>().ClientActivateDeathCam();
                alive = false;
            }
        }
        else
        {
            Debug.Log("Im Dead!");
        }
    }
}

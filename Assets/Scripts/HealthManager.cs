using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (alive)
            transform.parent.gameObject.GetComponent<PlayerConnection>().ServerAddHealth();
    }

    public void takeDamage(float damage)
    {
        if (alive)
        {
            transform.parent.gameObject.GetComponent<PlayerConnection>().ServerTakeDamage(damage);
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

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
        if (transform.GetComponent<Rikayon>().hasAuthority == false)
            return;
        alive = true;
        curHealth = 10f;
        maxHealth = 10f;
        UIhealthBar.setMaxHealth(maxHealth, curHealth);
        fade.PopIn();
    }

    public void addHealth()
    {
        if (transform.GetComponent<Rikayon>().hasAuthority == false)
            return;
        if (alive)
            transform.parent.gameObject.GetComponent<PlayerConnection>().CmdAddHealth();
    }

    public void takeDamage(float damage)
    {
        if (transform.GetComponent<Rikayon>().hasAuthority == false)
            return;
        if (alive)
        {
            transform.parent.gameObject.GetComponent<PlayerConnection>().CmdTakeDamage(damage);
            if (curHealth <= 0f && alive)
            {
                fpc.SetActive(false);
                tpc.SetActive(false);
                transform.parent.gameObject.GetComponent<PlayerConnection>().CmdKillMyUnit();
                alive = false;
            }
        }
        else
        {
            Debug.Log("Im Dead!");
        }
    }
}

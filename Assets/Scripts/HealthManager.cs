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

    public DeathFade deathMenu;
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
        {
            maxHealth += 3f;
            curHealth += 5f;
            if (curHealth > maxHealth)
                curHealth = maxHealth;
            UIhealthBar.setMaxHealth(maxHealth, curHealth);
            fade.PopIn();
        }
    }

    public void takeDamage(float damage)
    {
        if (alive)
        {
            curHealth -= damage;
            UIhealthBar.takeDamage(damage);
            fade.PopIn();
            if (curHealth <= 0f && alive)
            {
                this.gameObject.GetComponent<Rikayon>().enabled = false;
                this.gameObject.GetComponent<Rikayon>().enabled = true;
                this.gameObject.GetComponent<Rikayon>().animator.SetTrigger("Die");
                this.gameObject.GetComponent<Rikayon>().enabled = false;
                fpc.SetActive(false);
                tpc.SetActive(false);
                dc.SetActive(true);
                alive = false;

                deathMenu.toggleDeathMenu();
            }
        }
    }
}

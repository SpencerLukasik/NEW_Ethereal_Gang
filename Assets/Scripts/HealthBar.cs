using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    // Start is called before the first frame update
    public void takeDamage(float damage)
    {
        healthBar.value -= damage;
    }

    public void setMaxHealth(float maxHealth, float curHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = curHealth;
    }
}

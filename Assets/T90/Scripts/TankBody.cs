using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour
{
    private float health = 150f;
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
        {
            transform.GetChild(16).GetChild(0).GetComponent<TankHeadMovement>().playStrikeMetal();
            takeDamage(5);
        }

        else if (hit.gameObject.tag == "Spine")
        {
            hit.gameObject.transform.parent = this.gameObject.transform;
            hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
            hit.gameObject.GetComponent<Spine>().active = false;
            hit.gameObject.GetComponent<Spine>().impaleSound();
            takeDamage(2);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            transform.GetChild(16).GetChild(0).GetComponent<TankHeadMovement>().playExplode();
            transform.GetChild(16).GetChild(0).GetComponent<TankHeadMovement>().hasTarget = false;
            Destroy(this.gameObject, .05f);
        }
    }
}

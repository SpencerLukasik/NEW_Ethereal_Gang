using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBody : MonoBehaviour
{
    private float health = 150f;
    public Transform target;

    public bool hasTarget = false;
    void Update()
    {
        if (hasTarget)
        {
            if (Vector3.Distance(transform.position, target.position) >= 25f)
            {
                transform.position += transform.forward*Time.deltaTime*4;
                var lookPos = target.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * .1f);
            }
        }
    }
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
            GameObject.Find("TankSpawner").GetComponent<TankSpawner>().decrementCurTanks();
            Destroy(this.gameObject, .05f);
        }
    }
}

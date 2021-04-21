using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private PlayerConnection playerConnection;
    void Start()
    {
        playerConnection = transform.parent.parent.GetComponent<PlayerConnection>();
    }
    // Start is called before the first frame update
    void OnCollisionEnter(Collision hit)
    {
            if (!(hit.transform.IsChildOf(transform)) && hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
            {
                transform.parent.gameObject.GetComponent<HealthManager>().takeDamage(hit.transform.parent.parent.parent.GetComponent<Rikayon>().DAMAGE);
            }
            else if (hit.gameObject.tag == "Spine" && hit.gameObject.GetComponent<Spine>().active && hit.gameObject.GetComponent<Spine>().parentObject != transform.parent.gameObject)
            {
                hit.gameObject.transform.parent = this.gameObject.transform;
                hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
                hit.gameObject.GetComponent<Spine>().active = false;
                transform.parent.gameObject.GetComponent<HealthManager>().takeDamage(2f);
                hit.gameObject.GetComponent<Spine>().impaleSound();
            }  
    }
}

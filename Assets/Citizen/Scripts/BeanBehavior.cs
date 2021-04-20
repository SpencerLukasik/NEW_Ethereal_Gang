using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBehavior : MonoBehaviour
{
    public Animator animator;
    public bool alive = true;

    // Update is called once per frame

    void OnCollisionEnter(Collision hit)
    {
        if (alive && hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
        {
            hit.transform.parent.parent.parent.parent.GetComponent<PlayerConnection>().CmdKillBean(this.gameObject);
            hit.transform.parent.parent.parent.GetComponent<Rikayon>().corpse.Add(this.gameObject);
        }
        else if (hit.gameObject.tag == "Spine")
        {
            hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
            if (alive)
                hit.transform.parent.parent.GetComponent<PlayerConnection>().CmdKillBean(this.gameObject);
            hit.transform.parent.parent.GetComponent<PlayerConnection>().CmdImplant(this.gameObject, hit.gameObject);
        }
        else if (!alive && hit.gameObject.tag == "PlayerEat")
        {
            if (!hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Contains(this.gameObject))
                hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Add(this.gameObject);
        }
    }
    
    void OnCollisionExit(Collision hit)
    {
        if (!alive && hit.gameObject.tag == "PlayerEat")
        {
            if (hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Contains(this.gameObject))
                hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Remove(this.gameObject);
        }
    }

    public void destroyThisBean()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(this.gameObject, 2.0f);
    }
}
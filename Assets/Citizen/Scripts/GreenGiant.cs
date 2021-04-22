using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGiant : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSrc;
    private AudioClip brutalImpale;
    private AudioClip impaleBean;
    public Material green;
    public bool alive = true;
    public bool eaten = false;
    private Rikayon player;


    void Start()
    {
        audioSrc = this.GetComponent<AudioSource>();
        brutalImpale = Resources.Load<AudioClip>("BrutalImpale");
        impaleBean = Resources.Load<AudioClip>("ImpaleBean");
    }
    void OnCollisionEnter(Collision hit)
    {
        if (alive && hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
        {
            //hit.transform.parent.parent.parent.parent.GetComponent<PlayerConnection>().ServerKillBean(this.gameObject);
            killBean();
            hit.transform.parent.parent.parent.GetComponent<Rikayon>().corpse.Add(this.gameObject);
            audioSrc.PlayOneShot(brutalImpale);
        }
        else if (hit.gameObject.tag == "Spine")
        {
            hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
            if (alive)
                //hit.transform.parent.parent.GetComponent<PlayerConnection>().ServerKillBean(this.gameObject);
                killBean();
            //hit.transform.parent.parent.GetComponent<PlayerConnection>().ServerImplant(this.gameObject, hit.gameObject);
             hit.transform.parent = transform;
            hit.gameObject.GetComponent<Spine>().active = false;
            audioSrc.PlayOneShot(impaleBean);
        }
        else if (!alive && !eaten && hit.gameObject.tag == "PlayerEat")
        {
            if (!hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Contains(this.gameObject))
            {
                hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Add(this.gameObject);
                player = hit.transform.parent.GetComponent<Rikayon>();
            }
        }
    }
    
    void OnCollisionExit(Collision hit)
    {
        if (!alive && !eaten && hit.gameObject.tag == "PlayerEat")
        {
            if (hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Contains(this.gameObject))
            {
                hit.transform.parent.gameObject.GetComponent<Rikayon>().corpse.Remove(this.gameObject);
                player = null;
            }
        }
    }

    public void killBean()
    {
        animator.SetBool("isDead", true);
        alive = false;
        if (transform.parent.GetComponent<CivilianMovement>() != null)
            transform.parent.GetComponent<CivilianMovement>().enabled = false;
        else if (transform.parent.GetComponent<Movement>() != null)
            transform.parent.GetComponent<Movement>().enabled = false;
    }

    public void eatBean()
    {
        eaten = true;
        player.transform.localScale = new Vector3(player.transform.localScale.x*2, player.transform.localScale.y*2, player.transform.localScale.z*2);
        player.transform.GetChild(2).GetComponent<Renderer>().material = green;
        animator.SetBool("isEaten", true);
        Destroy(this.gameObject, 2f);
    }
}
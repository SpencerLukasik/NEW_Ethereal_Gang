using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;
public class Spine : MonoBehaviour
{
    private float VELOCITY;
    public Vector3 direction;
    public GameObject parentObject;

    public bool active = true;
    private AudioClip impale;
    private AudioSource audioSrc;
    void Start()
    {
        parentObject = transform.parent.gameObject;
        VELOCITY = 25f + transform.parent.transform.localScale.x;
        //parentObject.transform.parent.GetComponent<PlayerConnection>().ServerSpawn(this.gameObject);
        audioSrc = this.GetComponent<AudioSource>();
        impale = Resources.Load<AudioClip>("Impale");
        //transform.parent = null;
        Invoke("DestroyMe", 1.2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
            transform.position = transform.position + (direction * VELOCITY) * Time.deltaTime;
    }

    void DestroyMe() 
    {
        //parentObject.transform.parent.GetComponent<PlayerConnection>().ServerDestroy(this.gameObject);
        Destroy(this.gameObject);
    }

    public void StopDestroyingMe()
    {
        CancelInvoke("DestroyMe");
    }

    public void impaleSound()
    {
        audioSrc.PlayOneShot(impale);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;
public class Bullet : MonoBehaviour
{
    public float VELOCITY = 120f;
    private Vector3 direction;
    public bool active = true;
    public float DAMAGE = 5f;
    void Start()
    {
        //transform.parent.GetComponent<HelicopterBehavior>().target.parent.GetComponent<PlayerConnection>().ServerSpawn(this.gameObject);
        Invoke("DestroyMe", 1.2f);
        direction = transform.parent.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
            transform.position = transform.position + (direction * VELOCITY) * Time.deltaTime;
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Shell")
        {
            hit.transform.parent.gameObject.GetComponent<HealthManager>().takeDamage(DAMAGE);
            //transform.parent.GetComponent<HelicopterBehavior>().target.parent.GetComponent<PlayerConnection>().ServerDestroy(this.gameObject);
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
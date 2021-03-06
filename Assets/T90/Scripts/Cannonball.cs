using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;
public class Cannonball : MonoBehaviour
{
    private float VELOCITY = 30f;
    public Vector3 direction;
    public bool active = true;
    private float DAMAGE = 40f;
    void Start()
    {
        Invoke("DestroyMe", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
            transform.position = transform.position + (direction * VELOCITY) * Time.deltaTime;
    }

    void DestroyMe() 
    {
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Shell")
        {
            hit.transform.parent.gameObject.GetComponent<HealthManager>().takeDamage(DAMAGE);
            DestroyMe();
        }
    }
}

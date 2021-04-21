using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHeadMovement : MonoBehaviour
{
    public Transform target;
    public bool hasTarget;
    public LayerMask playerMask;
    private RaycastHit hit;
    public GameObject cannonball;
    private GameObject barrel;
    private float cannonballCooldown = 0f;
    public Animator pewpew;
    void Start()
    {
        hasTarget = false;
        barrel = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasTarget) {
        if (cannonballCooldown <= 0f)
        {
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);

            var barrelLookPos = target.position - barrel.transform.position;
            rotation = Quaternion.LookRotation(barrelLookPos);
            rotation.y = transform.rotation.y;
            rotation.z = transform.rotation.z;
            barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, rotation, Time.deltaTime*1f);
        }
        if (cannonballCooldown > 0f)
            cannonballCooldown -= Time.deltaTime;
        else if (Physics.Raycast(barrel.transform.position, barrel.transform.TransformDirection(Vector3.forward), out hit, 100f, playerMask))
        {
            StartCoroutine(Shoot());
            cannonballCooldown = 4f;
        }
        Debug.DrawRay(barrel.transform.position, barrel.transform.TransformDirection(Vector3.forward)*100f, Color.green);
        }

    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
            transform.parent.parent.GetComponent<TankBody>().takeDamage(5);

        else if (hit.gameObject.tag == "Spine")
        {
            hit.gameObject.transform.parent = this.gameObject.transform;
            hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
            hit.gameObject.GetComponent<Spine>().active = false;
            transform.parent.parent.GetComponent<TankBody>().takeDamage(5);
        }
    }
    
    public IEnumerator Shoot()
    {
        float waitTime = .5f;

        while (true)
        {
            if (waitTime < 0f)
            {
                GameObject a = Instantiate(cannonball) as GameObject;
                a.transform.SetParent(barrel.transform);
                a.transform.localPosition = new Vector3(0f, 0f, 5.8f);
                a.transform.SetParent(transform.parent.parent.transform);
                a.GetComponent<Cannonball>().direction = barrel.transform.forward;
                pewpew.SetTrigger("Fire!");
                break;
            }
            waitTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    
}

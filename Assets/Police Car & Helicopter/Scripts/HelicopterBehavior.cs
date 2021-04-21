using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private RaycastHit hit;
    public LayerMask playerMask;
    public GameObject bullet;
    public Vector3 offset;
    public GameObject center;
    private Vector3 newDir;
    public bool hasTarget;
    private float health = 40f;

    private float shootCooldown = 0f;

    // Update is called once per frame
    void Start()
    {
        hasTarget = false;
    }
    void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)*100f, Color.green);
        if (hasTarget)
        {
            //newDir = Vector3.RotateTowards(transform.forward, target.position - transform.position + offset, Time.deltaTime*.14f, 0.0F);
            //transform.rotation = Quaternion.LookRotation(newDir);
            //transform.RotateAround(target.position, Vector3.up, 5 * Time.deltaTime);

            var lookPos = target.position - transform.position + offset;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * .5f);

            if (Vector3.Distance(transform.position, target.position) >= 30f)
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, 4f * Time.deltaTime);


            if (shootCooldown > 0f)
                shootCooldown -= Time.deltaTime;
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100f, playerMask))
            {
                StartCoroutine(Shoot());
                shootCooldown = .8f;
            }
        }
        else
        {
            transform.position += Vector3.forward * 5* Time.deltaTime;
            transform.RotateAround(center.transform.position, Vector3.up, 2.5f * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Appendage" && hit.transform.parent.parent.parent.GetComponent<Rikayon>().isAttacking)
            health -= 5f;

        else if (hit.gameObject.tag == "Spine")
        {
            hit.gameObject.transform.parent = this.gameObject.transform;
            hit.gameObject.GetComponent<Spine>().StopDestroyingMe();
            hit.gameObject.GetComponent<Spine>().active = false;
            health -= 2f;
        }
        

        if (health <= 0f)
            Destroy(this.gameObject);
    }

    public IEnumerator Shoot()
    {
        float waitTime = .5f;

        while (true)
        {
            if (waitTime < 0f)
            {
                GameObject a = Instantiate(bullet) as GameObject;
                a.transform.SetParent(this.transform);
                a.transform.localPosition = new Vector3(0f, 0f, 0f);
                a.transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);
                break;
            }
            waitTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}

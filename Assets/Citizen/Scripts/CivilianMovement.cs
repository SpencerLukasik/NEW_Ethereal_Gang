using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianMovement : MonoBehaviour
{

    public Transform TransformToAvoid;
    //NavMesh Agent variable
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject ChildGameObject;
    // Start is called before the first frame update
    public int minRange = 15;




    public float wanderRadius = 15;
    public float wanderTimer = 3;

    private float timer;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") TransformToAvoid = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") TransformToAvoid = null;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (ChildGameObject.GetComponent<BeanBehavior>().alive)
        {
            if (TransformToAvoid == null)
            {
                if (timer >= wanderTimer)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }
            }
            else
            {
                transform.LookAt(TransformToAvoid);
                float distance = Vector3.Distance(transform.position, TransformToAvoid.position);
                bool tooClose = distance < minRange;
                Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
                transform.Translate(direction * Time.deltaTime * agent.speed);

            }
        }
    }
}

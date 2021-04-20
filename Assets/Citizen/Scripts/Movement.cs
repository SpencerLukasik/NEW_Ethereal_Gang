using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform transformToFollow;
    //NavMesh Agent variable
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject ChildGameObject;
    public int minRange = 7;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") transformToFollow = other.transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") { }
    }

    void Update()
    {
        //Follow the player
        if (ChildGameObject.GetComponent<BeanBehavior>().alive)
        {
            transform.LookAt(transformToFollow);
            float distance = Vector3.Distance(transform.position, transformToFollow.position);
            bool tooClose = distance < minRange;
            Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
            transform.Translate(direction * Time.deltaTime * agent.speed);
        }
    }
}

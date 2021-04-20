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
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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

    void Update()
    {
        if (ChildGameObject.GetComponent<BeanBehavior>().alive)
        {
            if (TransformToAvoid == null) return;
            transform.LookAt(TransformToAvoid);
            float distance = Vector3.Distance(transform.position, TransformToAvoid.position);
            bool tooClose = distance < minRange;
            Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
            transform.Translate(direction * Time.deltaTime * agent.speed);
        }
    }
}

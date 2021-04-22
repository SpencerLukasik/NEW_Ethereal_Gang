using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform transformToFollow;
    //NavMesh Agent variable
    UnityEngine.AI.NavMeshAgent agent;
    private AudioSource audioSrc;
    private AudioClip fire;
    private AudioClip reload;
    public GameObject ChildGameObject;
    public GameObject bullet;
    
    public int minRange = 7;
    // Start is called before the first frame update

    public float wanderRadius = 15;
    public float wanderTimer = 3;
    private int count = 0;

    private float timer;
    private float shootTimer;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
        shootTimer = 1f;
        audioSrc = this.GetComponent<AudioSource>();
        fire = Resources.Load<AudioClip>("Fire");
        reload = Resources.Load<AudioClip>("Reload");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") transformToFollow = other.transform;
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
        //Follow the player
        if (ChildGameObject.GetComponent<BeanBehavior>().alive)
        {
            if (transformToFollow == null)
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
                shootTimer -= Time.deltaTime;
                if (shootTimer > 1.5f && shootTimer < 1.517)
                    audioSrc.PlayOneShot(reload);
                if (shootTimer <= 0)
                {
                    GameObject a = Instantiate(bullet) as GameObject;
                    a.transform.SetParent(this.transform);
                    a.transform.localPosition = new Vector3(.5f, 0f, .5f);
                    a.transform.rotation = transform.rotation * Quaternion.Euler(90, 0, 0);
                    audioSrc.PlayOneShot(fire);
                    if (count >= 2)
                    {
                        shootTimer = 2.5f;
                        count = 0;
                    }
                    else
                    {
                        shootTimer = .3f;
                        count += 1;
                    }
                }
                transform.LookAt(transformToFollow);
                float distance = Vector3.Distance(transform.position, transformToFollow.position);
                bool tooClose = distance < minRange;
                Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
                transform.Translate(direction * Time.deltaTime * agent.speed);
            }
        }
    }
}

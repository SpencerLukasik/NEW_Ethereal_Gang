using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;
public class Rikayon : MonoBehaviour
{
    //Camera Control
    public GameObject fps;
    public GameObject tps;
    private bool cameraToggle;

    //Runtime Interactions
    public GameObject Spine;
    public List<GameObject> bodies = new List<GameObject>();
    public TankSpawner tankSpawner;
    public BodyManager bodyManager;
    private int numEaten;

    //Animations and Controls
    public CharacterController controller;
    public Animator animator;
    private float animation_timer = 0f;
    private float eat_timer = 0f;
    Vector3 movement;
    private float x;
    private float z;
    private float y;

    //Constants and Traits
    public GameObject healthStuff;
    public float SPEED = 7f;
    public float GRAVITY = -3f;
    public float DAMAGE = 5f;

    //Ground Checking
    public Transform groundCheck;
    public LayerMask groundMask;

    //Logic Booleans
    public bool walking = false;
    public bool still = true;
    public bool trigger = false;
    public bool isAttacking = false;
    public List<GameObject> spineImg;
    private int curSpines;
    private int maxSpines;
    private int growth;

    void Start()
    {
        cameraToggle = true;
        fps.SetActive(cameraToggle);
        tps.SetActive(!cameraToggle);
        healthStuff.SetActive(true);
        GameObject.Find("canUIQuallityManager").GetComponent<UIQualityManager>().DisableOnStart();
        numEaten = 0;
        curSpines = 3;
        maxSpines = 8;
        growth = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (eat_timer > 0)
        {
            eat_timer -= Time.deltaTime;
            return;
        }
        //Shoot spine
        if (Input.GetKeyDown(KeyCode.E) && curSpines > 0)
        {
            spawnSpine();
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            cameraToggle = !cameraToggle;
            fps.SetActive(cameraToggle);
            tps.SetActive(!cameraToggle);
        }

        else if (Input.GetMouseButtonDown(2) && still)
		{
            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodies[i].GetComponent<BeanBehavior>().alive == false)
                {
                    bodies[i].GetComponent<BeanBehavior>().eatBean();
                    numEaten += 1;
                    bodies.RemoveAt(i);
                    i -= 1;
                }
            }
            if (numEaten <= 0)
                return;

            animator.SetTrigger("Eat_Cycle_1");
			animation_timer = 2f;
            eat_timer = 1.5f;
            this.gameObject.GetComponent<HealthManager>().addHealth(numEaten);
            DAMAGE += .1f * numEaten;
            StartCoroutine(grow(numEaten));
            renewSpines(numEaten);
            bodyManager.DisableBeanReminder();
            bodyManager.AddToBodyCount(numEaten);
            numEaten = 0;

            if (growth%7 == 0)
                tankSpawner.incrementMaxTanks();
		}

        else if (isGrounded() && movement.y < 0)
        {
            if (Input.GetKey(KeyCode.Space))
                movement.y = (Mathf.Log(transform.localScale.y)+2);
            else
                movement.y = -2f;
			//If the animation timer is at 0, we can conduct an action
			if (animation_timer <= 0f)
			{
				//List of actions
				if (Input.GetMouseButtonDown(0))
				{
            		//serverConnection.ServerUpdateAnimation("Attack_2");
                    animator.SetTrigger("Attack_2");
					animation_timer = 1f;
                    isAttacking = true;
        		}
				else if (Input.GetMouseButtonDown(1))
				{
					//serverConnection.ServerUpdateAnimation("Attack_3");
                    animator.SetTrigger("Attack_3");
					animation_timer = 1f;
                    isAttacking = true;
				}
			}
			else
            {
				animation_timer -= Time.deltaTime;
                if (animation_timer <= 0f)
                {
                    trigger = true;
                    isAttacking = false;
                }
            }
			
        }
        //If we are walking, and we were still previously, and we are not in the middle of an animation,
        //Animate walk cycle
        if (walking && trigger && animation_timer <= 0f)
        {
            //serverConnection.ServerUpdateAnimation("Walk_Cycle_1");
            animator.SetTrigger("Walk_Cycle_1");
            trigger = false;
        }
        else if (still && trigger && animation_timer <= 0f)
        {
            //serverConnection.ServerUpdateAnimation("Rest_1");
            animator.SetTrigger("Rest_1");
            trigger = false;
        }
		
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        y = movement.y;
        movement = transform.right * x * transform.localScale.x + transform.forward * z * transform.localScale.z;
        movement.y = y + GRAVITY * Time.deltaTime;
        if ((movement.x > .05f || movement.z > .05f) || ((movement.x < -.05f || movement.z < -.05f)))
        {
            walking = true;
            if (still)
            {
                still = false;
                trigger = true;
            }
        }
        else
        {
            walking = false;
            if (!still)
            {
                still = true;
                trigger = true;
            }
        }

        controller.Move(movement * SPEED * Time.deltaTime);
    }

    public void renewSpines(int numBodies)
    {
        for (int i = 0; i < numBodies*4; i++)
        {
            if (curSpines >= maxSpines)
                break;

            spineImg[curSpines].SetActive(true);
            curSpines += 1;
        }
    }

    private bool isGrounded()
    {
        return Physics.CheckBox(groundCheck.position, new Vector3(transform.localScale.x, .15f+transform.localScale.y*.1f, transform.localScale.y), Quaternion.identity, groundMask);
    }

    private void spawnSpine()
    {
        curSpines -= 1;
        spineImg[curSpines].SetActive(false);
        GameObject a = Instantiate(Spine) as GameObject;

        a.transform.parent = transform;
        if (fps.activeSelf)
            a.GetComponent<Spine>().direction = fps.transform.forward;
        else
            a.GetComponent<Spine>().direction = tps.transform.forward;

        a.transform.position = fps.transform.position + new Vector3(0f, -.2f, 0f);
        a.transform.rotation = fps.transform.rotation * Quaternion.Euler(90, 0, 0);
    }

    public IEnumerator grow(int bodies)
    {
        for (int i = 0; i < 100; i++)
        {
            transform.localScale += new Vector3(.001f, .001f, .001f)*bodies;
            yield return new WaitForEndOfFrame();
        }
    }
}

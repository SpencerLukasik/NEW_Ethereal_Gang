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
    private PlayerConnection serverConnection;
    public List<GameObject> corpse = new List<GameObject>();

    //Animations and Controls
    public CharacterController controller;
    public Animator animator;
    private float animation_timer = 0f;
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

    void Start()
    {
        SoundManagerScript.PlayAmbiance();
        //StartCoroutine(DelayEnableObjects(5));
        //serverConnection = transform.parent.GetComponent<PlayerConnection>();
        GameObject.Find("TitleScreenCamera").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Only move personal character
        //if (hasAuthority == false)
        //    return;

        //Shoot spine
        if (Input.GetKeyDown(KeyCode.E))
        {
            spawnSpine();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            cameraToggle = !cameraToggle;
            fps.SetActive(cameraToggle);
            tps.SetActive(!cameraToggle);
        }

        if (isGrounded() && movement.y < 0)
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
            		serverConnection.ServerUpdateAnimation("Attack_2");
					animation_timer = 1f;
                    isAttacking = true;
        		}
				else if (Input.GetMouseButtonDown(1))
				{
					serverConnection.ServerUpdateAnimation("Attack_3");
					animation_timer = 1f;
                    isAttacking = true;
				}
                else if (Input.GetMouseButtonDown(2) && corpse.Count > 0 && still)
				{
                    serverConnection.ServerEatBean(corpse[0]);
                    corpse.Remove(corpse[0]);
					animation_timer = 2f;
                    StartCoroutine(grow());
                    this.gameObject.GetComponent<HealthManager>().addHealth();
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
            serverConnection.ServerUpdateAnimation("Walk_Cycle_1");
            trigger = false;
        }
        else if (still && trigger && animation_timer <= 0f)
        {
            serverConnection.ServerUpdateAnimation("Rest_1");
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

    private bool isGrounded()
    {
        return Physics.CheckBox(groundCheck.position, new Vector3(transform.localScale.x, .15f+transform.localScale.y*.1f, transform.localScale.y), Quaternion.identity, groundMask);
    }

    private void spawnSpine()
    {
        GameObject a = Instantiate(Spine) as GameObject;

        a.transform.parent = transform;
        if (fps.activeSelf)
            a.GetComponent<Spine>().direction = fps.transform.forward;
        else
            a.GetComponent<Spine>().direction = tps.transform.forward;

        a.transform.position = fps.transform.position + new Vector3(0f, -.2f, 0f);
        a.transform.rotation = fps.transform.rotation * Quaternion.Euler(90, 0, 0);
    }

    public IEnumerator grow()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.localScale += new Vector3(.001f, .001f, .001f);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator DelayEnableObjects(float frames)
    {
        for (int i = 0; i < frames; i++)
            yield return new WaitForEndOfFrame();
        //if (hasAuthority)
        //{
            cameraToggle = true;
            fps.SetActive(cameraToggle);
            tps.SetActive(!cameraToggle);
            healthStuff.SetActive(true);
        //}
    }
}
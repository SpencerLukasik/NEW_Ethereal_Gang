using UnityEngine;
using System.Collections;
public class TitleCamera : MonoBehaviour {
 
    public GameObject target;//the target object
    public GameObject Player;
    public GameObject thisCam;
    public GameObject Helicopter;
    public GameObject Tank;
    public GameObject Title;
    public GameObject Menu;
    public Camera FirstPersonCam;
    private float speedMod = 10.0f;//a speed modifier
    private Vector3 point;//the coord to the point where the camera looks at
    
     public bool FPCamera = false;

    void Start () {//Set up things on the start method
        point = target.transform.position;//get target's coords
        transform.LookAt(point);//makes the camera look to it 
    }
 
    void Update () {//makes the camera rotate around "point" coords, rotating around its Y axis, 20 degrees per second times the speed modifier
        transform.RotateAround (target.transform.position, Vector3.up, speedMod * Time.deltaTime);  
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.SetActive(true);  
            Helicopter.SetActive(true);
            Tank.SetActive(true);
            Menu.SetActive(true);

            thisCam.SetActive(false);
            Title.SetActive(false);
            FPCamera = true;
            FirstPersonCam.gameObject.SetActive(FPCamera);
        }
    }


}
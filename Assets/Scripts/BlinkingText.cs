using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class BlinkingText : MonoBehaviour {
        public GameObject Player;
        public GameObject Title; 
        public GameObject HealthBar;
        public GameObject TitleCamera;
        public GameObject AvatarScreen;
        public GameObject AvatarCam;
        public GameObject PlayerMouseMvmt;

        public float timer; 
        public static bool activated = false;
        public bool FPCamera = false;
        public void NewGame()
       {
            if (Input.GetKeyDown(KeyCode.Space) && activated == false)
            { 
                activated = true;   
                HealthBar.SetActive(false);
                Player.SetActive(true);
                PlayerMouseMvmt.SetActive(false);

                Title.SetActive(false);
                FPCamera = false;
                TitleCamera.SetActive(false);
                transform.gameObject.SetActive(false);
                AvatarScreen.SetActive(true);
                AvatarCam.SetActive(true);
            }
       }
        
       void Update()
       {
           HealthBar.SetActive(false);
           NewGame();

           timer = timer + Time.deltaTime;
           if(timer >= 0.5)
           {
                   GetComponent<Text>().enabled = true;
           }
           if(timer >= 1)
           {
                   GetComponent<Text>().enabled = false;
                   timer = 0;
           }
       }
   
}
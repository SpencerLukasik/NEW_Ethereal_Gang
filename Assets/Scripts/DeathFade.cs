using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class DeathFade : MonoBehaviour
{
    public Image BackgroundImg;
    private bool isShown = false;
    private float transition = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isShown){
            return;
        }

        transition+= Time.deltaTime;
        BackgroundImg.color = Color.Lerp(new Color(0,0,0,0), Color.black, transition);
    }

    public void toggleDeathMenu(){
        gameObject.SetActive(true);
        isShown = true;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

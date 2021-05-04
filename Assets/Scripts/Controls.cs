using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject feast;
    public GameObject controls;
    public GameObject panel;
    public Text myText;

	public void TaskOnClick(){
		if (feast.activeSelf)
        {
            panel.SetActive(true);
            controls.SetActive(true);
            feast.SetActive(false);
            myText.text = "Title";
        }
        else
        {   
            panel.SetActive(false);
            controls.SetActive(false);
            feast.SetActive(true);
            myText.text = "Controls";
        }
	}
}

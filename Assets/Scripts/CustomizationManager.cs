using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationManager : MonoBehaviour
{
    enum appearanceDetails{
        ACCESSORIES_MODEL,
        SKIN_COLOR
    }
 
    //setActives
    public GameObject HealthBar; 
    public GameObject AvatarScreen;
    public GameObject AvatarCam;
    public GameObject PlayerMouseMvmt;
    public GameObject Controls;
    public bool FPCamera = false;
    [SerializeField] private SkinnedMeshRenderer skinColorRender;
    [SerializeField] private Image previewcrossHair;
    [SerializeField] private Image FPcrossHair;
    [SerializeField] private Image TPcrossHair;

    //HeadAccessories
    public GameObject[] headAccessories;
    public Color32[] skinColor;
    public Color32[] crosshairColor;
    public GameObject[] randomizedArray;

    public int skinColorIndex = 0;
    public int accessoryIndex = 0;
    public int crosshairColorIndex = 0;
     
    public void Randomize(){
        // Debug.Log("Clicked");
        // //GameObject randomize = randomizedArray[Random.Range(0, headAccessories.Length)]; 
        // var randomheadIndex= (int)(Random.value * headAccessories.Length);
        // var randomskinIndex= (int)(Random.value * skinColor.Length);
        // var randomcrossIndex= (int)(Random.value * crosshairColor.Length);
        // // Random.Range(0,skinColor.Length);
        // // Random.Range(0,crosshairColor.Length);
    }
    public void nextcrosshairColor(){
        //skinColor[skinColorIndex].SetActive(false);
        
        previewcrossHair.color=crosshairColor[crosshairColorIndex];
        FPcrossHair.color=crosshairColor[crosshairColorIndex];
        TPcrossHair.color=crosshairColor[crosshairColorIndex];
        crosshairColorIndex = (crosshairColorIndex + 1) % crosshairColor.Length;
        //skinColor[skinColorIndex].SetActive(true);
    }
    public void previouscrosshairColor(){
 
        crosshairColorIndex--;

        if(crosshairColorIndex < 0){
            crosshairColorIndex += crosshairColor.Length;
        }

        //skinColor[skinColorIndex].SetActive(true);
    }
    public void nextSkinColor(){
        //skinColor[skinColorIndex].SetActive(false);
        
        skinColorRender.material.color=skinColor[skinColorIndex];
        skinColorIndex = (skinColorIndex + 1) % skinColor.Length;
        //skinColor[skinColorIndex].SetActive(true);
    }
    public void previousSkinColor(){
        //skinColorRender.material.color=skinColor[skinColorIndex];
        //skinColor[skinColorIndex].SetActive(false);
        skinColorIndex--;

        if(skinColorIndex < 0){
            skinColorIndex += skinColor.Length;
        }

        //skinColor[skinColorIndex].SetActive(true);
    }

    public void nextAccessory(){
        headAccessories[accessoryIndex].SetActive(false);
        accessoryIndex = (accessoryIndex + 1) % headAccessories.Length;
        headAccessories[accessoryIndex].SetActive(true);
    }

    public void previousAccessory(){
        headAccessories[accessoryIndex].SetActive(false);
        accessoryIndex--;

        if(accessoryIndex < 0){
            accessoryIndex += headAccessories.Length;
        }

        headAccessories[accessoryIndex].SetActive(true);
    }

    public void StartGame(){
        PlayerPrefs.SetInt("accessoryIndex", accessoryIndex);
        PlayerPrefs.SetInt("skinColorIndex", skinColorIndex); 
        PlayerPrefs.SetInt("crosshairColorIndex", crosshairColorIndex); 

        HealthBar.SetActive(true);
        PlayerMouseMvmt.SetActive(true);
        FPCamera = true; 
        
        transform.gameObject.SetActive(false);
        AvatarScreen.SetActive(false);
        AvatarCam.SetActive(false);
        Controls.SetActive(false);
    } 
}

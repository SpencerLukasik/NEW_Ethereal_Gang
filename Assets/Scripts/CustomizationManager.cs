using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //HeadAccessories
    public GameObject[] headAccessories;
    public Color32[] skinColor;

    public int skinColorIndex = 0;
    public int accessoryIndex = 0;

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

        HealthBar.SetActive(true);
        PlayerMouseMvmt.SetActive(true);
        FPCamera = true; 
        
        transform.gameObject.SetActive(false);
        AvatarScreen.SetActive(false);
        AvatarCam.SetActive(false);
        Controls.SetActive(false);
    } 
}

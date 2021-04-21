using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public List<GameObject> hats;
    public List<Material> colors;
    public List<GameObject> spawnPoints;
    public GameObject beanPrefab;
    private float timer;
    void Start()
    {
        timer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            spawnBean();
            timer = 3f;
        }
    }

    void spawnBean()
    {
        GameObject newBean = Instantiate(beanPrefab) as GameObject;
        int randomHatRange = Random.Range(0, 12);
        int randomColorRange = Random.Range(0, 6);
        GameObject randomHat = Instantiate(hats[randomHatRange]) as GameObject;
        randomHat.transform.parent = newBean.transform.GetChild(0);
        newBean.transform.GetChild(0).GetComponent<Renderer>().material = colors[randomColorRange];
        newBean.transform.parent = GameObject.Find("Citizens").transform;
    }
}

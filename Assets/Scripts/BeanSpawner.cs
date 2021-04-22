using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawner : MonoBehaviour
{
    public List<GameObject> hats;
    public List<Material> colors;
    public List<GameObject> spawnPoints;
    public GameObject beanPrefab;
    float maxBeans;
    private float timer;
    void Start()
    {
        timer = 4f;
        maxBeans = transform.childCount;
        Debug.Log(maxBeans);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (transform.childCount < maxBeans)
                spawnBean();
            timer = 2f;
            Debug.Log(transform.childCount);
        }
    }

    void spawnBean()
    {
        GameObject newBean = Instantiate(beanPrefab) as GameObject;
        int randomHatRange = Random.Range(0, 13);
        int randomColorRange = Random.Range(0, 6);
        int randomSpawnPoint = Random.Range(0, 40);
        GameObject randomHat = Instantiate(hats[randomHatRange]) as GameObject;
        randomHat.transform.parent = newBean.transform.GetChild(0);
        newBean.transform.GetChild(0).GetComponent<Renderer>().material = colors[randomColorRange];
        newBean.transform.parent = GameObject.Find("SpawnPoints").transform;
        newBean.transform.localPosition = spawnPoints[randomSpawnPoint].transform.localPosition;
        newBean.transform.parent = GameObject.Find("Citizens").transform;
        Debug.Log("I was spawned at " + (randomSpawnPoint+1).ToString());
    }
}

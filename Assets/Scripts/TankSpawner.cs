using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tank;
    public List<GameObject> spawnPoints;
    private int curTanks;
    private int maxTanks;
    private float timer;
    void Start()
    {
        curTanks = 1;
        maxTanks = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            if (curTanks < maxTanks)
            {
                GameObject a = Instantiate(tank) as GameObject;
                a.transform.position = spawnPoints[Random.Range(0, 6)].transform.position;
                curTanks += 1;
                if (curTanks > 2)
                {
                    a.transform.GetChild(16).GetChild(0).GetComponent<TankHeadMovement>().target = GameObject.Find("PlayerPrefab").transform;
                    a.transform.GetChild(16).GetChild(0).GetComponent<TankHeadMovement>().hasTarget = true;
                    a.GetComponent<TankBody>().target = GameObject.Find("PlayerPrefab").transform;
                    a.GetComponent<TankBody>().hasTarget = true;
                }
            }
            timer = 4f;
        }
    }

    public void decrementCurTanks()
    {
        curTanks -= 1;
    }

    public void incrementMaxTanks()
    {
        maxTanks += 1;
    }
}

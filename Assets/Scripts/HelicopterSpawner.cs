using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tank;
    private int curTanks;
    private int maxTanks;
    private float timer;
    void Start()
    {
        curTanks = 0;
        maxTanks = 50;
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
            }
        }
    }

    public void incrementMaxTanks()
    {
        maxTanks += 1;
    }
}

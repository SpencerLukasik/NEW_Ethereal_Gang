using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAwareness : MonoBehaviour
{
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Shell")
        {
            transform.GetChild(0).GetComponent<TankHeadMovement>().target = hit.transform;
            transform.GetChild(0).GetComponent<TankHeadMovement>().hasTarget = true;
            transform.parent.GetComponent<TankBody>().target = hit.transform;
            transform.parent.GetComponent<TankBody>().hasTarget = true;
        }
    }
}
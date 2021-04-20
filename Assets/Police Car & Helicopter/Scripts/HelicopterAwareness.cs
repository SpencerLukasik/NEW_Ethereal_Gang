using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterAwareness : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Shell")
        {
            transform.parent.GetComponent<HelicopterBehavior>().target = hit.transform.parent.transform;
            transform.parent.GetComponent<HelicopterBehavior>().hasTarget = true;
            transform.parent.GetComponent<HelicopterBehavior>().offset = hit.transform.parent.localScale;
        }
    }
}

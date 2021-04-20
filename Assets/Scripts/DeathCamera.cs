using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private Vector3 offset;

    void Start()
    {
        target = transform.parent.gameObject.transform;
        transform.LookAt(target);
        offset = target.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newDir = Vector3.RotateTowards(transform.forward, target.position - transform.position + offset, Time.deltaTime*.2f, 0.0F); 
        transform.rotation = Quaternion.LookRotation(newDir);
        transform.RotateAround(target.position, Vector3.up, 5 * Time.deltaTime);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awareness : MonoBehaviour
{
    BeanBehavior _bean;

    void Start()
    {
        // store a reference to the guard script this awareness works with
        _bean = transform.parent.GetComponent<BeanBehavior>();
    }

    // when something enters our awareness collider, let the guard know
    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.name == "Player")
            //_bean.OnAware(other.transform);
            Debug.Log("Hello!");
    }
}

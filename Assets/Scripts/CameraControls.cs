using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float SENSATIVITY = 3000f;
    private float xRotation = 0f;
    public Transform playerBody;
    private float mouseX;
    private float mouseY;
    public float yClamp = -80f;
    public float zClamp = 70f;

    void start()
    {
        //if (transform.parent.GetComponent<Rikayon>().hasAuthority == false)
        //    return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (transform.parent.GetComponent<Rikayon>().hasAuthority == false)
        //    return;
        mouseX = Input.GetAxis("Mouse X") * SENSATIVITY; //* Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * SENSATIVITY; //* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, yClamp, zClamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

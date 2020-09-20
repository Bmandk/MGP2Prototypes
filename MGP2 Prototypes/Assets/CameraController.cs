using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float mouseSensitivity = 100;
    private float xRotation;
    private Vector3 offset;
     
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        offset = target.transform.position - transform.position;
    }
    
    void LateUpdate()
    {
        
        /*float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);*/
        
        float horizontal = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;
        target.transform.Rotate(0, horizontal, 0);
 
        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * offset);
         
        transform.LookAt(target.transform);
    }
}

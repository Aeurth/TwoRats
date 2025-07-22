using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public float distanceFromPlayer = 3f;
    public float heightOffset = 1.5f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    bool locked = true;

    private void LateUpdate()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distanceFromPlayer) + Vector3.up * heightOffset;
        transform.position = player.position + offset;
        transform.LookAt(player.position + Vector3.up * heightOffset);

        if (locked && transform.position.y > 85f)
        {
            transform.position = new Vector3(transform.position.x, 85f, transform.position.z);
        }
    }

    public void ChangeState(bool state)
    {
        locked = state;
    }


}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject button;
    void clicked()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        button.SetActive(false);

    }


}

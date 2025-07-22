using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    [Header("refs")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject UIManager;
    [SerializeField] GameObject ExitUI;
    [SerializeField] GameObject PlayCamera;

    [Header("collectibles settings")]
    [SerializeField] int maxBooksCount;
    [SerializeField] int maxCheezeCount;

    int booksCount = 0;
    int cheezeCount = 0;
    UIControler uiControler;
    MovePlayerRB playerMovement;
    CameraControler cameraControler;

    private void Awake()
    {
        Player.OnItemCollected += UpdateUI;
    }
    private void Start()
    {
        Application.targetFrameRate = 120;
        QualitySettings.vSyncCount = 0; // Disable VSync to allow frame rate control
        uiControler = UIManager.GetComponent<UIControler>();
        playerMovement = player.GetComponent<MovePlayerRB>();
        cameraControler = PlayCamera.GetComponent<CameraControler>();
        playerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (ExitUI.activeSelf) Continue();
            else Pause();
        }
    }
    public void OnStartClicked()
    {
        Debug.Log("Game manager onCLick");
        playerMovement.enabled = true;
        uiControler.TurnOnUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
    }

    private void UpdateUI(ItemType type)
    {
        if(type == ItemType.book)
        {
            booksCount++;
            uiControler.UpdateCount(type, booksCount.ToString() + "/" + maxBooksCount.ToString());
            return;
        }
        if(type == ItemType.cheeze) {
            Debug.Log("cheeze recieved");
            cheezeCount++;
            uiControler.UpdateCount(type, cheezeCount.ToString() + "/" + maxCheezeCount.ToString());
        }
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // Stops the game in the editor
        #else
            Application.Quit();  // Exits the game in a build
        #endif
    }
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ExitUI.SetActive(false);
        playerMovement.enabled = true;
        cameraControler.enabled = true;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ExitUI.SetActive(true); 
        playerMovement.enabled = false;
        cameraControler.enabled = false;

    }
}

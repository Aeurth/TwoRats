using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    cheeze,
    book
}
public class UIControler : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CheezeCount;
    [SerializeField] TextMeshProUGUI BooksCount;
    [SerializeField] GameObject contentFitter;


    private void Start()
    {
        CheezeCount.text = "0/0";
        BooksCount.text = "0/0";
    }
    public void UpdateCount(ItemType type, string message)
    {
        Debug.Log(message);
        if (type == ItemType.cheeze)
        {
            CheezeCount.text = message;
            return;
        }
        if (type == ItemType.book)
        {
            BooksCount.text = message;
            return;
        }
    }
    public void TurnOnUI()
    {
        Debug.Log("UI turned on");
        contentFitter.SetActive(true);
    }
    
}


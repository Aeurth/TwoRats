using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<ItemType> OnItemCollected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Cheeze"))
        {
            Destroy(other.gameObject);
            OnItemCollected?.Invoke(ItemType.cheeze);
            return;
        }
        if(other.gameObject.CompareTag("Book"))
        {
            Destroy(other.gameObject);
            OnItemCollected?.Invoke(ItemType.book);
            return;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;

    private void Awake()
    {
        if (SingletonManager.Instance != null)
        {
            inventory = SingletonManager.Instance.inventory;
        }
        else
        {
            Debug.LogError("SibletonManager Instance not found");
        }

    }
}

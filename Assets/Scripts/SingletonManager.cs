using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    public static SingletonManager Instance { get; private set; }

    public Inventory inventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            inventory = new Inventory(20); // Initialize with the desired number of slots
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
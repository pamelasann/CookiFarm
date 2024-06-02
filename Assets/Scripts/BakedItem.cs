using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakedItem : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            SingletonManager.Instance.inventory.Add(this);
            SingletonManager.Instance.RefreshUI(); // Refresh UI immediately
            Destroy(gameObject);
        }
    }
}

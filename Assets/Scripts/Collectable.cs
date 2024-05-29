using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable : MonoBehaviour
{
    public CollectableType type;
    public Sprite icon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            SingletonManager.Instance.inventory.Add(this);
            SingletonManager.Instance.RefreshUI();
            Destroy(gameObject);
        }

    }
}


public enum CollectableType
{
    NONE, TRIGO_DONE, FRESA_DONE, LECHE_NUMS, EGG_NUMS, COOKED_LECHE
}

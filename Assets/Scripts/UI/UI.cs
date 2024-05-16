using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;

    public List<Slots> slots = new List<Slots>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            //SetUp();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    /*
    void SetUp()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].type != CollectableType.NONE)
                {

                }
            }
        }
    }
    */
}

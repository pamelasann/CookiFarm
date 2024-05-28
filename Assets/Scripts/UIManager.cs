using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;

    private void OnEnable()
    {
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        Inventory inventory = SingletonManager.Instance.inventory;

        foreach (Inventory.Slot slot in inventory.slots)
        {
            if (slot.type != CollectableType.NONE)
            {
                GameObject slotGO = Instantiate(slotPrefab, inventoryPanel.transform);
                slotGO.transform.Find("ItemIcon").GetComponent<Image>().sprite = slot.icon;
                slotGO.transform.Find("ItemCount").GetComponent<Text>().text = slot.count.ToString();
            }
        }
    }
}
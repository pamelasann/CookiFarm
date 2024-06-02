using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BakingManager : MonoBehaviour
{
    public GameObject strawberryPiePrefab;
    public GameObject breadPrefab;
    public GameObject cookiePrefab;

    public GameObject burntStrawberryPiePrefab;
    public GameObject burntBreadPrefab;
    public GameObject burntCookiePrefab;

    public Transform bakingArea;
    public float bakingTime = 5f;
    public float burningTime = 10f;

    public TextMeshProUGUI messageText;
    public Button pickUpButton;
    public Button waitButton;
    public GameObject bakingPanel;

    private bool isBaking = false;
    private bool isItemReady = false;
    private CollectableType currentItemType;
    private bool itemCollected = false;

    public void StartBaking(CollectableType type)
    {
        Debug.Log("Attempting to start baking: " + type);
        if (!isBaking)
        {
            if (HasRequiredItems(type))
            {
                Debug.Log("Has required items for: " + type);
                RemoveRequiredItems(type);
                StartCoroutine(BakingProcess(type));
            }
            else
            {
                Debug.Log("Insufficient items for: " + type);
                ShowMessage("Insufficient items to bake.");
            }
        }
    }

    private IEnumerator BakingProcess(CollectableType type)
    {
        isBaking = true;
        currentItemType = type;
        isItemReady = false;
        itemCollected = false;
        ShowMessage("Item is baking...");

        yield return new WaitForSeconds(bakingTime);

        if (isBaking)
        {
            CreateBakedItem(type);
            isItemReady = true;
            ShowMessage("Item is ready! Pick up or wait?");
            EnableButtons(true);
        }

        yield return new WaitForSeconds(burningTime - bakingTime);

        if (isBaking && isItemReady && !itemCollected)
        {
            BurnItem(type);
            ShowMessage("Item burnt!");
            EnableButtons(false);
        }

        isBaking = false;
    }

    private bool HasRequiredItems(CollectableType type)
    {
        Inventory inventory = SingletonManager.Instance.inventory;
        bool hasItems = false;

        switch (type)
        {
            case CollectableType.Bread:
                hasItems = inventory.HasItems(CollectableType.EGG_NUMS, 2) && inventory.HasItems(CollectableType.TRIGO_DONE, 4);
                Debug.Log($"Checking items for Bread: EGG_NUMS=2 ({inventory.HasItems(CollectableType.EGG_NUMS, 2)}), TRIGO_DONE=4 ({inventory.HasItems(CollectableType.TRIGO_DONE, 4)})");
                break;
            case CollectableType.Cookie:
                hasItems = inventory.HasItems(CollectableType.MILK_NUMS, 2) && inventory.HasItems(CollectableType.EGG_NUMS, 4) && inventory.HasItems(CollectableType.TRIGO_DONE, 6);
                Debug.Log($"Checking items for Cookie: MILK_NUMS=2 ({inventory.HasItems(CollectableType.MILK_NUMS, 2)}), EGG_NUMS=4 ({inventory.HasItems(CollectableType.EGG_NUMS, 4)}), TRIGO_DONE=6 ({inventory.HasItems(CollectableType.TRIGO_DONE, 6)})");
                break;
            case CollectableType.StrawberryPie:
                hasItems = inventory.HasItems(CollectableType.FRESA_DONE, 2) && inventory.HasItems(CollectableType.Cookie, 2) && inventory.HasItems(CollectableType.MILK_NUMS, 4) && inventory.HasItems(CollectableType.EGG_NUMS, 8) && inventory.HasItems(CollectableType.TRIGO_DONE, 12);
                Debug.Log($"Checking items for StrawberryPie: Cookie=2 ({inventory.HasItems(CollectableType.Cookie, 2)}), MILK_NUMS=4 ({inventory.HasItems(CollectableType.MILK_NUMS, 4)}), EGG_NUMS=8 ({inventory.HasItems(CollectableType.EGG_NUMS, 8)}), TRIGO_DONE=12 ({inventory.HasItems(CollectableType.TRIGO_DONE, 12)})");
                break;
            default:
                hasItems = false;
                break;
        }

        return hasItems;
    }

    private void RemoveRequiredItems(CollectableType type)
    {
        Inventory inventory = SingletonManager.Instance.inventory;
        switch (type)
        {
            case CollectableType.Bread:
                inventory.RemoveItems(CollectableType.EGG_NUMS, 2);
                inventory.RemoveItems(CollectableType.TRIGO_DONE, 4);
                break;
            case CollectableType.Cookie:
                inventory.RemoveItems(CollectableType.MILK_NUMS, 2);
                inventory.RemoveItems(CollectableType.EGG_NUMS, 4);
                inventory.RemoveItems(CollectableType.TRIGO_DONE, 6);
                break;
            case CollectableType.StrawberryPie:
                inventory.RemoveItems(CollectableType.FRESA_DONE, 2);
                inventory.RemoveItems(CollectableType.Cookie, 2);
                inventory.RemoveItems(CollectableType.MILK_NUMS, 4);
                inventory.RemoveItems(CollectableType.EGG_NUMS, 8);
                inventory.RemoveItems(CollectableType.TRIGO_DONE, 12);
                break;
        }
    }

    private void CreateBakedItem(CollectableType type)
    {
        GameObject bakedItemPrefab = null;
        switch (type)
        {
            case CollectableType.Bread:
                bakedItemPrefab = breadPrefab;
                break;
            case CollectableType.Cookie:
                bakedItemPrefab = cookiePrefab;
                break;
            case CollectableType.StrawberryPie:
                bakedItemPrefab = strawberryPiePrefab;
                break;
        }

        if (bakedItemPrefab != null)
        {
            Instantiate(bakedItemPrefab, bakingArea.position, Quaternion.identity);
        }

        ShowMessage("Baked item added!");
        Debug.Log("Baked item created: " + type);
    }

    private void BurnItem(CollectableType type)
    {
        GameObject burntItemPrefab = null;
        switch (type)
        {
            case CollectableType.Bread:
                burntItemPrefab = burntBreadPrefab;
                break;
            case CollectableType.Cookie:
                burntItemPrefab = burntCookiePrefab;
                break;
            case CollectableType.StrawberryPie:
                burntItemPrefab = burntStrawberryPiePrefab;
                break;
        }

        if (burntItemPrefab != null)
        {
            Instantiate(burntItemPrefab, bakingArea.position, Quaternion.identity);
            AddItemToInventory(type, true);  // Add the burnt item to inventory
        }

        isItemReady = false;
        ShowMessage("Burnt item added!");
        Debug.Log("Burnt item created: " + type);
    }

    public void PickUpItem()
    {
        if (isItemReady)
        {
            AddItemToInventory(currentItemType, false);
            ShowMessage("Well done!");
            EnableButtons(false);
            itemCollected = true;  // Mark the item as collected
            Debug.Log("Item picked up: " + currentItemType);
        }
    }

    public void WaitForItem()
    {
        if (isItemReady)
        {
            BurnItem(currentItemType);
            ShowMessage("Item burnt!");
            EnableButtons(false);
            Debug.Log("Item burnt: " + currentItemType);
        }
    }

    private void AddItemToInventory(CollectableType type, bool isBurnt)
    {
        Inventory inventory = SingletonManager.Instance.inventory;
        GameObject itemPrefab = isBurnt ? GetBurntItemPrefab(type) : GetBakedItemPrefab(type);

        if (itemPrefab != null)
        {
            Collectable item = itemPrefab.GetComponent<Collectable>();
            inventory.Add(item);
            SingletonManager.Instance.RefreshUI();
            Debug.Log((isBurnt ? "Burnt" : "Baked") + " item added to inventory: " + type);
        }
    }

    private GameObject GetBakedItemPrefab(CollectableType type)
    {
        switch (type)
        {
            case CollectableType.Bread:
                return breadPrefab;
            case CollectableType.Cookie:
                return cookiePrefab;
            case CollectableType.StrawberryPie:
                return strawberryPiePrefab;
            default:
                return null;
        }
    }

    private GameObject GetBurntItemPrefab(CollectableType type)
    {
        switch (type)
        {
            case CollectableType.Bread:
                return burntBreadPrefab;
            case CollectableType.Cookie:
                return burntCookiePrefab;
            case CollectableType.StrawberryPie:
                return burntStrawberryPiePrefab;
            default:
                return null;
        }
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void EnableButtons(bool enable)
    {
        if (pickUpButton != null)
        {
            pickUpButton.gameObject.SetActive(enable);
        }

        if (waitButton != null)
        {
            waitButton.gameObject.SetActive(enable);
        }
    }

    public void CloseBakingPanel()
    {
        if (bakingPanel != null)
        {
            bakingPanel.SetActive(false);
        }
    }
}
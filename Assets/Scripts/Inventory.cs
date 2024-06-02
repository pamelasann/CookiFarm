using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count;
        public int maxAllowed;
        public Sprite icon;

        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            maxAllowed = 99;
        }
        public bool CanAddItem()
        {
            return count < maxAllowed;
        }
        public void Additem(Collectable item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;
        }
        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
            }
        }
    }
    public List<Slot> slots = new List<Slot>();
    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    public void Add(Collectable item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == item.type && slot.CanAddItem())
            {
                slot.Additem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == CollectableType.NONE)
            {
                slot.Additem(item);
                return;
            }
        }
    }
    // New method to check if the inventory has a specified quantity of a particular type
    public bool HasItems(CollectableType type, int count)
    {
        int totalCount = 0;
        foreach (Slot slot in slots)
        {
            if (slot.type == type)
            {
                totalCount += slot.count;
            }
            if (totalCount >= count)
            {
                return true;
            }
        }
        return false;
    }
    // New method to remove a specified quantity of a particular type from the inventory
    public void RemoveItems(CollectableType type, int count)
    {
        foreach (Slot slot in slots)
        {
            if (slot.type == type && slot.count >= count)
            {
                slot.count -= count;
                if (slot.count == 0)
                {
                    slot.type = CollectableType.NONE;
                    slot.icon = null;
                }
                return;
            }
        }
        // If the exact slot doesn't have enough items, we need to deduct from multiple slots
        int remainingCount = count;
        foreach (Slot slot in slots)
        {
            if (slot.type == type)
            {
                if (slot.count <= remainingCount)
                {
                    remainingCount -= slot.count;
                    slot.count = 0;
                    slot.type = CollectableType.NONE;
                    slot.icon = null;
                }
                else
                {
                    slot.count -= remainingCount;
                    remainingCount = 0;
                    break;
                }
            }
        }
    }
}
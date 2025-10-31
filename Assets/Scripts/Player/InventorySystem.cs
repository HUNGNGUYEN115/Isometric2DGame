using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public List<Items> inventory = new List<Items>();

    // Event: called when item added
    public Action<Items> OnItemAdded;

    public void AddItem(Items newItem)
    {
        inventory.Add(newItem);
        Debug.Log($"Picked up {newItem.itemname}");

        OnItemAdded?.Invoke(newItem); // notify UI
    }

    public void UseItem(int index, PlayerControllers player)
    {
        Items item = inventory[index];
        switch (item.itemtype)
        {
            case Itemtype.Potion:
                {
                    if (item is HealthPotion potion)
                    {
                        player.Health(potion.healamount);
                        inventory.RemoveAt(index);
                    }

                    break;
                }
        }
    }
}

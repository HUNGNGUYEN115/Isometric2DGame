using System;
using UnityEngine;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public Items[] inventory = new Items[6];         // 6 fixed slots
    public PlayerControllers player;
    public PlayerAim gun;

    public Action<Items, int> OnItemChanged;         // Notifies UI which slot changed
    public TextMeshProUGUI text;                     // For showing item info

    // ---------------------------
    // Add Item
    // ---------------------------
    public void AddItem(Items newItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                Debug.Log($"Picked up {newItem.itemname} into slot {i}");

                OnItemChanged?.Invoke(newItem, i);
                return;
            }
        }

        Debug.Log("Inventory is full!");
    }

    // ---------------------------
    // Use Item
    // ---------------------------
    public void UseItem(int index)
    {
        if (!IsValidSlot(index)) return;

        Items item = inventory[index];
        if (item == null) return;

        switch (item.itemtype)
        {
            case Itemtype.Potion:
                {
                    if (item is HealthPotion potion)
                    {
                        player.Health(potion.healamount);
                        inventory[index] = null;
                        OnItemChanged?.Invoke(null, index);
                    }
                    break;
                }

            case Itemtype.Weapon:
                {
                    if (item is Weapon weapon)
                    {
                        gun.bulletPrefab = weapon.bullet;
                        gun.GetComponent<SpriteRenderer>().sprite = weapon.shape;
                        player.damage = weapon.damage;

                        inventory[index] = null;
                        OnItemChanged?.Invoke(null, index);
                    }
                    break;
                }
        }
    }

    // ---------------------------
    // Drop Item
    // ---------------------------
    public void DropItem(int index)
    {
        if (!IsValidSlot(index)) return;

        Items item = inventory[index];
        if (item == null) return;

        Vector3 dropPos = player.transform.position;

        switch (item.itemtype)
        {
            case Itemtype.Potion:
                {
                    if (item is HealthPotion potion)
                        Instantiate(potion.potion, dropPos, Quaternion.identity);
                    break;
                }

            case Itemtype.Weapon:
                {
                    if (item is Weapon weapon)
                        Instantiate(weapon.gunprefab, dropPos, Quaternion.identity);
                    break;
                }
        }

        inventory[index] = null; // Empty the slot
        OnItemChanged?.Invoke(null, index);
    }

    // ---------------------------
    // Item Info (Tooltip)
    // ---------------------------
    public void Infoitem(int index)
    {
        if (!IsValidSlot(index)) return;

        Items item = inventory[index];
        if (item == null)
        {
            text.text = "";
            return;
        }

        text.text = item.info;
    }

    // ---------------------------
    // Helper
    // ---------------------------
    private bool IsValidSlot(int index)
    {
        return index >= 0 && index < inventory.Length;
    }
}

using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public List<Items> inventory = new List<Items>();
    public PlayerControllers player;
    public PlayerAim gun;
    // Event: called when item added
    public Action<Items> OnItemAdded;
    public TextMeshProUGUI text;

    public void AddItem(Items newItem)
    {
        inventory.Add(newItem);
        Debug.Log($"Picked up {newItem.itemname}");

        OnItemAdded?.Invoke(newItem); // notify UI
    }

    public void UseItem(int index)
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
            case Itemtype.Weapon:
                {
                    //Change the properties of the current gun
                    if (item is Weapon weapon)
                    {
                        gun.bulletPrefab = weapon.bullet;
                        SpriteRenderer gunsprite = gun.GetComponent<SpriteRenderer>();
                        gunsprite.sprite = weapon.shape;
                        player.damage=weapon.damage;    

                    }
                    break;

                }
        }
    }
    public void DropItem(int index)
    {
        Items item = inventory[index];
        switch (item.itemtype)
        {
            //Spawn items at the player's positon
            case Itemtype.Potion:
                {
                    if (item is HealthPotion potion)
                    {
                        Instantiate(potion.potion, player.transform.position, Quaternion.identity);
                        inventory.RemoveAt(index);
                    }

                    break;
                }
            case Itemtype.Weapon:
                {

                    if (item is Weapon weapon)
                    {
                        Instantiate(weapon.gunprefab, player.transform.position, Quaternion.identity);
                        
                        inventory.RemoveAt(index);
                        
                    }
                    break;

                }
        }
        
    }
    public void Infoitem(int index)
    {
        Items item = inventory[index];
        text.text = item.info;

    }
}

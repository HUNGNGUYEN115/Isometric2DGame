using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;


public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public Button useButton;
    public List<Button> slotButtons;
    public Sprite defaultSprite;
    public int selectedslot=-1;

    private void OnEnable()
    {
        if (inventorySystem != null)
            inventorySystem.OnItemAdded += UpdateUI;
    }

    private void OnDisable()
    {
        if (inventorySystem != null)
            inventorySystem.OnItemAdded -= UpdateUI;
    }

    private void UpdateUI(Items newItem)
    {
        Debug.Log($"Updating UI for {newItem.itemname}"); //  Debug here

        foreach (Button slot in slotButtons)
        {
            Image img = slot.GetComponent<Image>();
            if (img.sprite == defaultSprite)
            {
                img.sprite = newItem.icon;
                
                return;
            }
        }

       
    }
    public void SelectSlot(int index)
    {
        selectedslot = index;

        Image img = slotButtons[index-1].GetComponent<Image>();
        if (img.sprite != null && img.sprite != defaultSprite)
        {
            useButton.interactable = true;
            
        }
        else
        {
            useButton.interactable = false;
            
        }
    }
    public void UseSelectedItem(PlayerControllers player)
    {
        inventorySystem.UseItem(selectedslot-1, player);
        Image img = slotButtons[selectedslot - 1].GetComponent<Image>();
        img.sprite = defaultSprite;
        useButton.interactable = false;

    }
}

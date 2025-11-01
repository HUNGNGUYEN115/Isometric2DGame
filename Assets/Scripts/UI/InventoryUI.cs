using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine.EventSystems;


public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public Button useButton;
    public Button dropButton;
    public List<Button> slotButtons;
    public Sprite defaultSprite;
    public int selectedslot;
    public GameObject Infopanel;

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
            dropButton.interactable = true;
            Infopanel.SetActive(true);


        }
        else
        {
            useButton.interactable = false;
            dropButton.interactable = false;
            Infopanel.SetActive(false);

        }
    }
    public void UseSelectedItem()
    {
        inventorySystem.UseItem(selectedslot-1);
        Image img = slotButtons[selectedslot - 1].GetComponent<Image>();
        img.sprite = defaultSprite;
        useButton.interactable = false;
        dropButton.interactable = false;

    }
    public void DropSelectedItem()
    {
        inventorySystem.DropItem(selectedslot - 1);
        Image img = slotButtons[selectedslot - 1].GetComponent<Image>();
        img.sprite = defaultSprite;
        useButton.interactable = false;
        dropButton.interactable = false;

    }

    public void ShowItemInfo(int index)
    {
        if (index < 0 || index >= inventorySystem.inventory.Count)
            return;

        inventorySystem.Infoitem(index);
        Infopanel.SetActive(true);
    }

    public void HideItemInfo()
    {
        Infopanel.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public InventorySystem inventorySystem;

    public Button useButton;
    public Button dropButton;

    public List<Button> slotButtons;     // 6 buttons
    public Sprite defaultSprite;

    public int selectedslot = -1;
    public GameObject Infopanel;
    //FXSound
    public AudioClip clipSound;
    private void OnEnable()
    {
        inventorySystem.OnItemChanged += RefreshSlot;
    }

    private void OnDisable()
    {
        inventorySystem.OnItemChanged -= RefreshSlot;
    }

    // --------------------------------------------
    // Refresh ONE slot when item changes
    // --------------------------------------------
    private void RefreshSlot(Items item, int slotIndex)
    {
        Image img = slotButtons[slotIndex].GetComponent<Image>();

        if (item == null)
            img.sprite = defaultSprite;
        else
            img.sprite = item.icon;
    }

    // --------------------------------------------
    // Player clicks a slot
    // --------------------------------------------
    public void SelectSlot(int index)
    {
        selectedslot = index-1;
        SoundFXManager.Instance.PlaySound(clipSound, transform);
        Items item = inventorySystem.inventory[index-1];
        bool hasItem = item != null;

        useButton.interactable = hasItem;
        dropButton.interactable = hasItem;
        Infopanel.SetActive(hasItem);

        if (hasItem)
            inventorySystem.Infoitem(index-1);
    }

    // --------------------------------------------
    // Use item
    // --------------------------------------------
    public void UseSelectedItem()
    {
        if (selectedslot < 0) return;
        SoundFXManager.Instance.PlaySound(clipSound, transform);
        inventorySystem.UseItem(selectedslot);

        // UI will auto update from OnItemChanged
        useButton.interactable = false;
        dropButton.interactable = false;
    }

    // --------------------------------------------
    // Drop item
    // --------------------------------------------
    public void DropSelectedItem()
    {
        if (selectedslot < 0) return;
        SoundFXManager.Instance.PlaySound(clipSound, transform);
        inventorySystem.DropItem(selectedslot);

        useButton.interactable = false;
        dropButton.interactable = false;
    }

    // --------------------------------------------
    // Hover tooltip
    // --------------------------------------------
    public void ShowItemInfo(int index)
    {
        if (index < 0 || index >= inventorySystem.inventory.Length)
            return;

        Items item = inventorySystem.inventory[index];

        if (item == null)
        {
            Infopanel.SetActive(false);
            return;
        }

        inventorySystem.Infoitem(index);
        Infopanel.SetActive(true);
    }

    public void HideItemInfo()
    {
        Infopanel.SetActive(false);
    }
}

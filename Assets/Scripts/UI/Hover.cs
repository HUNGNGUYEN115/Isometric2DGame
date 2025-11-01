using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotIndex;
    public InventoryUI inventoryUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryUI != null)
        {
            inventoryUI.ShowItemInfo(slotIndex-1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryUI != null)
        {
            inventoryUI.HideItemInfo();
        }
    }
}

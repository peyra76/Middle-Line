using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    private ItemData itemData;

    public void Setup(ItemData data)
    {
        itemData = data;
        iconImage.sprite = data.itemIcon;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryManager.Instance.ShowTooltip(itemData.description, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.HideTooltip();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image iconImage;

    private ItemData itemData;

    public void Setup(ItemData data)
    {
        itemData = data;
        iconImage.sprite = data.itemIcon;
    }
}
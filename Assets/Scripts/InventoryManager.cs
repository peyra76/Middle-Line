using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Panels")]
    public GameObject inventoryPanel;

    [Header("Grid Settings")]
    public Transform itemGrid;
    public GameObject slotPrefab;

    private List<ItemData> items = new List<ItemData>();
    private bool isOpen = false;

    void Awake() => Instance = this;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddItem(ItemData newData)
    {
        items.Add(newData);
        GameObject newSlot = Instantiate(slotPrefab, itemGrid);
        newSlot.GetComponent<InventorySlot>().Setup(newData);
    }
}

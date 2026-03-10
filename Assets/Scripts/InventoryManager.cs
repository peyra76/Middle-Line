using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("UI Panels")]
    public GameObject inventoryPanel;
    public GameObject tooltipPanel;
    public Text tooltipText;

    [Header("Grid Settings")]
    public Transform itemGrid;
    public GameObject slotPrefab;

    private List<ItemData> items = new List<ItemData>();
    private bool isOpen = false;

    void Awake() => Instance = this;

    void Start()
    {
        inventoryPanel.SetActive(false);
        tooltipPanel.SetActive(false);
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
            HideTooltip();
        }
    }

    public void AddItem(ItemData newData)
    {
        items.Add(newData);
        GameObject newSlot = Instantiate(slotPrefab, itemGrid);
        newSlot.GetComponent<InventorySlot>().Setup(newData);
    }

    public void ShowTooltip(string description, Vector3 position)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = description;
        tooltipPanel.transform.position = position + new Vector3(100, -50, 0); 
    }

    public void HideTooltip() => tooltipPanel.SetActive(false);
}
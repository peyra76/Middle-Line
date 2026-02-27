using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<ItemData> allPossibleItems; 
    public GameObject baseItemPrefab;       

    void Start()
    {
        SpawnRandomItem();
    }

    public void SpawnRandomItem()
    {
        if (allPossibleItems.Count == 0) return;

        int randomIndex = Random.Range(0, allPossibleItems.Count);
        ItemData selectedItem = allPossibleItems[randomIndex];

        GameObject spawnedObj = Instantiate(baseItemPrefab, transform.position, Quaternion.identity);

        PickupItem pickupScript = spawnedObj.GetComponent<PickupItem>();
        pickupScript.data = selectedItem;

        Instantiate(selectedItem.visualPrefab, spawnedObj.transform);
    }
}
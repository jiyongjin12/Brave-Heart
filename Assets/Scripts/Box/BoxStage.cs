using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxStage : MonoBehaviour
{
    public GameObject Box;
    [SerializeField] private List<ItemSO> item = new List<ItemSO>();
    [SerializeField] private Transform SpawnPos;
    [SerializeField] private BoxItem SpawnItem;
    [SerializeField] private InventoryTesting Inventory;

    private void Start()
    {
        SpawnRandomItems();
    }

    private void SpawnRandomItems()
    {
        List<ItemSO> randomItems = GetRandomItems(3);

        foreach (ItemSO selectedItem in randomItems)
        {
            BoxItem newBoxItem = Instantiate(SpawnItem, SpawnPos);
            newBoxItem.Initialize(selectedItem, this);
        }
    }

    private List<ItemSO> GetRandomItems(int count)
    {
        List<ItemSO> randomItems = new List<ItemSO>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, item.Count);
            randomItems.Add(item[randomIndex]);
        }

        return randomItems;
    }

    public void OnBoxItemClick(ItemSO clickedItem)
    {
        Box.SetActive(false);
        Inventory.ItemInfo.Add(clickedItem);
    }

    public void OnBox()
    {
        Box.SetActive(true);
    }
}

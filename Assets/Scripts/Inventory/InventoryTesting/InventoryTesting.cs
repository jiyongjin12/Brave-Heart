using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTesting : MonoBehaviour
{
    [SerializeField]
    private Transform outerInventoryTetrisBackground;
    [SerializeField]
    private Inventory inventoryTetris;
    [SerializeField]
    private Inventory outerInventoryTetris;
    [SerializeField]
    private List<string> addItemTetrisSaveList;

    [SerializeField]
    private List<ItemSO> ItemInfo;
    private int currentItemIndex = 0;

    private int addItemTetrisSaveListIndex;

    private void Start()
    {
        outerInventoryTetrisBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            outerInventoryTetrisBackground.gameObject.SetActive(true);
            outerInventoryTetris.Load(addItemTetrisSaveList[addItemTetrisSaveListIndex]);

            addItemTetrisSaveListIndex = (addItemTetrisSaveListIndex + 1) % addItemTetrisSaveList.Count;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(inventoryTetris.Save());
            inventoryTetris.SaveToFile();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Check");
            inventoryTetris.LoadFromFile();
        }

        if (Input.GetKeyDown(KeyCode.R)) // 아이템 소환
        {
            ExportIncomingItems();
        }
    }

    private void ExportIncomingItems()
    {
        List<ItemSO> itemsToPlace = new List<ItemSO>(ItemInfo);

        while (itemsToPlace.Count > 0)
        {
            ItemSO currentItem = itemsToPlace[0];
            itemsToPlace.RemoveAt(0);

            bool itemPlaced = false;
            int attempts = 0;
            int maxAttempts = 100; // 최대 시도 횟수 설정

            while (!itemPlaced && attempts < maxAttempts)
            {
                attempts++;
                Vector2Int randomPosition = GetRandomGridPosition(outerInventoryTetris.GetGrid().GetWidth(), outerInventoryTetris.GetGrid().GetHeight());

                if (outerInventoryTetris.TryPlaceItem(currentItem, randomPosition, PlacedObjectTypeSO.Dir.Down))
                {
                    itemPlaced = true;
                    Debug.Log($"Item placed at: {randomPosition}");
                }
            }

            if (!itemPlaced)
            {
                Debug.LogWarning($"Could not place item: {currentItem.name}, no valid position found.");
            }
        }
    }

    private Vector2Int GetRandomGridPosition(int gridWidth, int gridHeight)
    {
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);
        return new Vector2Int(x, y);
    }
}

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
    public List<ItemSO> ItemInfo;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Check");
            inventoryTetris.LoadFromFile();
        }
    }

    public void ExportIncomingItems()
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

        ItemInfo.Clear();
    }

    private Vector2Int GetRandomGridPosition(int gridWidth, int gridHeight)
    {
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);
        return new Vector2Int(x, y);
    }
}

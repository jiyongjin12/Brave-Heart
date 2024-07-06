using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }


    public event EventHandler<PlacedObject> OnObjectPlaced;

    private Grid<GridObject> grid;
    private RectTransform itemContainer;

    private const string SaveFilePath = "inventoryTetrisSave.json";

    [SerializeField]
    private bool InventorySaveCheck;

    private void Awake()
    {
        Instance = this;

        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 60f;
        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));

        itemContainer = transform.Find("ItemContainer").GetComponent<RectTransform>();

        transform.Find("BackgroundTempVisual").gameObject.SetActive(false);
    }

    public class GridObject
    {

        private Grid<GridObject> grid;
        private int x;
        private int y;
        public PlacedObject placedObject;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString()
        {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }

        public bool HasPlacedObject()
        {
            return placedObject != null;
        }

    }

    public Grid<GridObject> GetGrid()
    {
        return grid;
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        grid.GetXY(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public bool IsValidGridPosition(Vector2Int gridPosition)
    {
        return grid.IsValidGridPosition(gridPosition);
    }


    public bool TryPlaceItem(ItemSO itemTetrisSO, Vector2Int placedObjectOrigin, PlacedObjectTypeSO.Dir dir)
    {
        // Test Can Build
        List<Vector2Int> gridPositionList = itemTetrisSO.GetGridPositionList(placedObjectOrigin, dir);
        bool canPlace = true;
        foreach (Vector2Int gridPosition in gridPositionList)
        {
            bool isValidPosition = grid.IsValidGridPosition(gridPosition);
            if (!isValidPosition)
            {
                // 유효하지 않음
                canPlace = false;
                break;
            }
            if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
            {
                canPlace = false;
                break;
            }
        }

        if (canPlace)
        {
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    canPlace = false;
                    break;
                }
            }
        }

        if (canPlace)
        {
            Vector2Int rotationOffset = itemTetrisSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, rotationOffset.y) * grid.GetCellSize();

            PlacedObject placedObject = PlacedObject.CreateCanvas(itemContainer, placedObjectWorldPosition, placedObjectOrigin, dir, itemTetrisSO);
            placedObject.transform.rotation = Quaternion.Euler(0, 0, -itemTetrisSO.GetRotationAngle(dir));

            placedObject.GetComponent<InventoryDragDrop>().Setup(this);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }

            OnObjectPlaced?.Invoke(this, placedObject);

            SaveToFile();
            //Debug.Log("Update");

            // 배치!
            return true;
        }
        else
        {
            // 배치 불가!
            return false;
        }
    }

    public void RemoveItemAt(Vector2Int removeGridPosition)
    {
        PlacedObject placedObject = grid.GetGridObject(removeGridPosition.x, removeGridPosition.y).GetPlacedObject();

        if (placedObject != null)
        {
            // Demolish
            placedObject.DestroySelf();

            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }

            SaveToFile();
            //Debug.Log("Delete");

        }
    }

    public RectTransform GetItemContainer()
    {
        return itemContainer;
    }



    [Serializable]
    public struct AddItemTetris
    {
        public string itemTetrisSOName;
        public Vector2Int gridPosition;
        public PlacedObjectTypeSO.Dir dir;
    }

    [Serializable]
    public struct ListAddItemTetris
    {
        public List<AddItemTetris> addItemTetrisList;
    }

    public string Save()
    {
        List<PlacedObject> placedObjectList = new List<PlacedObject>();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                if (grid.GetGridObject(x, y).HasPlacedObject())
                {
                    placedObjectList.Remove(grid.GetGridObject(x, y).GetPlacedObject());
                    placedObjectList.Add(grid.GetGridObject(x, y).GetPlacedObject());
                }
            }
        }

        List<AddItemTetris> addItemTetrisList = new List<AddItemTetris>();
        foreach (PlacedObject placedObject in placedObjectList)
        {
            addItemTetrisList.Add(new AddItemTetris
            {
                dir = placedObject.GetDir(),
                gridPosition = placedObject.GetGridPosition(),
                itemTetrisSOName = (placedObject.GetPlacedObjectTypeSO() as ItemSO).name,
            });

        }

        return JsonUtility.ToJson(new ListAddItemTetris { addItemTetrisList = addItemTetrisList });
    }

    public void Load(string loadString)
    {
        ResetInventory();

        ListAddItemTetris listAddItemTetris = JsonUtility.FromJson<ListAddItemTetris>(loadString);

        foreach (AddItemTetris addItemTetris in listAddItemTetris.addItemTetrisList)
        {
            TryPlaceItem(InventoryAssets.Instance.GetItemTetrisSOFromName(addItemTetris.itemTetrisSOName), addItemTetris.gridPosition, addItemTetris.dir);
        }
    }


    //=========================================================================================
    // 세이브

    //public void SaveToFile()
    //{
    //    string saveString = Save();
    //    File.WriteAllText(Path.Combine(Application.persistentDataPath, SaveFilePath), saveString);
    //    Debug.Log("Inventory saved to " + Path.Combine(Application.persistentDataPath, SaveFilePath));
    //}

    //public void LoadFromFile()
    //{
    //    string saveFilePath = Path.Combine(Application.persistentDataPath, SaveFilePath);
    //    if (File.Exists(saveFilePath))
    //    {
    //        string loadString = File.ReadAllText(saveFilePath);
    //        Load(loadString);
    //        Debug.Log("Inventory loaded from " + saveFilePath);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Save file not found at " + saveFilePath);
    //    }
    //}

    //public void ResetInventory()
    //{
    //    for (int x = 0; x < grid.GetWidth(); x++)
    //    {
    //        for (int y = 0; y < grid.GetHeight(); y++)
    //        {
    //            if (grid.GetGridObject(x, y).HasPlacedObject())
    //            {
    //                RemoveItemAt(new Vector2Int(x, y));
    //            }
    //        }
    //    }

    //    string saveFilePath = Path.Combine(Application.persistentDataPath, SaveFilePath);
    //    if (File.Exists(saveFilePath))
    //    {
    //        File.Delete(saveFilePath);
    //        Debug.Log("Save file deleted from " + saveFilePath);
    //    }
    //}


    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, $"{name}_inventoryTetrisSave.json");
    }

    public void SaveToFile()
    {
        string saveString = Save();
        string saveFilePath = GetSaveFilePath();
        File.WriteAllText(saveFilePath, saveString);
        Debug.Log("Inventory saved to " + saveFilePath);
    }

    public void LoadFromFile()
    {
        string saveFilePath = GetSaveFilePath();
        if (File.Exists(saveFilePath))
        {
            string loadString = File.ReadAllText(saveFilePath);
            Load(loadString);
            Debug.Log("Inventory loaded from " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at " + saveFilePath);
        }
    }

    public void ResetInventory()
    {
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                if (grid.GetGridObject(x, y).HasPlacedObject())
                {
                    RemoveItemAt(new Vector2Int(x, y));
                }
            }
        }

        string saveFilePath = GetSaveFilePath();
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted from " + saveFilePath);
        }
    }
}

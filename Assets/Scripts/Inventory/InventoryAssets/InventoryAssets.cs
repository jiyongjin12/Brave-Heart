using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAssets : MonoBehaviour
{
    public static InventoryAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public ItemSO[] itemTetrisSOArray;

    public ItemSO ammo;
    //public ItemTetrisSO grenade;
    //public ItemTetrisSO katana;
    //public ItemTetrisSO medkit;
    //public ItemTetrisSO pistol;
    //public ItemTetrisSO rifle;
    //public ItemTetrisSO shotgun;
    //public ItemTetrisSO money;

    public ItemSO GetItemTetrisSOFromName(string itemTetrisSOName)
    {
        foreach (ItemSO itemTetrisSO in itemTetrisSOArray)
        {
            if (itemTetrisSO.name == itemTetrisSOName)
            {
                return itemTetrisSO;
            }
        }
        return null;
    }


    public Sprite gridBackground;
    public Sprite gridBackground_2;
    public Sprite gridBackground_3;

    public Transform gridVisual;
}

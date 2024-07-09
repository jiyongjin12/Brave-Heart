using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnOff : MonoBehaviour
{
    public static InventoryOnOff inventoryOnOff { get; private set; }

    public Inventory inventory;
    public InventoryTesting inventoryTest;

    [SerializeField]
    private GameObject Inventory;
    [SerializeField]
    private bool OnOffCheck = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && OnOffCheck == true)
        {
            OnInventory();
            OnOffCheck = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && OnOffCheck == false)
        {
            Inventory.SetActive(false);
            OnOffCheck = true;
        }
    }

    public void OnOffButton()
    {
        if(OnOffCheck == true)
        {
            OnInventory();
            OnOffCheck = false;
        }
        else
        {
            Inventory.SetActive(false);
            OnOffCheck = true;
        }
    }

    public void OnInventory()
    {
        Inventory.gameObject.SetActive(true);
        inventoryTest.ExportIncomingItems();
        inventory.LoadFromFile();
    }

}

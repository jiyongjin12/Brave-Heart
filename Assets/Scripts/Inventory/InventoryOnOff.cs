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

    private void Awake()
    {
        inventoryOnOff = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && OnOffCheck == true)
        {
            OnInventory();
            OnOffCheck = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) && OnOffCheck == false)
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
        inventoryTest.ExportIncomingItems(); // 임시 인벤에 저장된 리스트 넣기
        inventory.LoadFromFile(); // 인벤의 저장을 로드해옴
    }

}

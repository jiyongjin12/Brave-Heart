using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShopCanvas : MonoBehaviour
{
    public static ShopCanvas instance { get; private set; }

    [SerializeField] private GameObject Shop; // 상점
    [SerializeField] private Transform ItemPropLocation; // 소환 위치
    [SerializeField] private ItemProp ItemProp; // 소환 프리팹
    [SerializeField] private Shop_so ItemList;
    [SerializeField] private int numberOfItemsToDisplay = 6; // 상점에 표시할 아이템 갯수
    [SerializeField] private InventoryTesting Inventory;

    [SerializeField] private int ReRollPrice = 50;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private readonly List<ItemProp> _currentItemPropsList = new List<ItemProp>();

    public int Gold = 500;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PopulateShop();
    }


    public void RerollShop()
    {
        //if (Gold >= ReRollPrice)                              // 임시로 넣은 골드
        if (GameSuvManager.instance.Gold >= ReRollPrice)
        {
            ClearShop(); // 기존 아이템 프롭 삭제
            PopulateShop(); // 다시 아이템 프롭 생성

            //Gold -= ReRollPrice;                              // 임시로 넣은 골드
            GameSuvManager.instance.Gold -= ReRollPrice;
            ReRollPrice += (ReRollPrice / 2);
        }
        else
        {
            Debug.Log("No Money");
        }

        
    }

    private void PopulateShop()
    {
        //int itemsToCreate = numberOfItemsToDisplay - _currentItemPropsList.Count(item => item.PurchasedItem);

        //for (int i = 0; i < itemsToCreate; i++)
        //{
        //    var itemSO = GetRandomItem(ItemList.ShopItem);
        //    var itemPropInstance = Instantiate(ItemProp, ItemPropLocation);
        //    itemPropInstance.item = itemSO;

        //    // Set the itemPropInstance properties
        //    itemPropInstance.itemProp.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
        //    itemPropInstance.Pride = Random.Range(itemSO.MinPrice, itemSO.MaxPrice + 1);
        //    itemPropInstance.Goldtext.text = itemPropInstance.Pride.ToString();
        //    itemPropInstance.Inventory = Inventory;
        //    itemPropInstance.itemSprite.sprite = itemSO.Test; // Sprite 설정

        //    _currentItemPropsList.Add(itemPropInstance);
        //}

        int itemsToCreate = numberOfItemsToDisplay - _currentItemPropsList.Count(item => item.PurchasedItem);

        for (int i = 0; i < itemsToCreate; i++)
        {
            var itemSO = GetRandomItem(ItemList.ShopItem);
            var itemPropInstance = Instantiate(ItemProp, ItemPropLocation);
            itemPropInstance.item = itemSO;

            // Set the itemPropInstance properties
            itemPropInstance.itemProp.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));
            itemPropInstance.Pride = Random.Range(itemSO.MinPrice, itemSO.MaxPrice + 1);
            itemPropInstance.Goldtext.text = itemPropInstance.Pride.ToString();
            itemPropInstance.Inventory = Inventory;
            itemPropInstance.itemSprite.sprite = itemSO.Test; // Sprite 설정

            _currentItemPropsList.Add(itemPropInstance);
        }
    }

    private ItemSO GetRandomItem(List<ItemSO> itemList)
    {
        int randomIndex = Random.Range(0, itemList.Count);
        return itemList[randomIndex];
    }

    private void ClearShop()
    {
        //foreach (var itemProp in _currentItemPropsList)
        //{
        //    Destroy(itemProp.gameObject);
        //}
        //_currentItemPropsList.Clear();

        for (int i = _currentItemPropsList.Count - 1; i >= 0; i--)
        {
            if (!_currentItemPropsList[i].PurchasedItem)
            {
                Destroy(_currentItemPropsList[i].gameObject);
                _currentItemPropsList.RemoveAt(i);
            }
        }
    }
}

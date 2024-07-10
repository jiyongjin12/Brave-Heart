using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicPanel : MonoBehaviour
{
    [SerializeField] private ItemListings i;
    [SerializeField] private GameObject Displayitem;
    [SerializeField] private int itemsToDisplay = 8; // Displayitem에 표시할 아이템 수
    [SerializeField] private UnityEngine.UI.Image itemPrefab; // UI 이미지 프리팹

    private void Start()
    {
        DisplayRandomItems();
    }

    private void DisplayRandomItems()
    {
        List<ItemSO> selectedItems = SelectRandomItems(itemsToDisplay);

        foreach (ItemSO item in selectedItems)
        {
            // UI 이미지를 생성하고 Displayitem의 자식으로 설정
            UnityEngine.UI.Image newItemDisplay = Instantiate(itemPrefab, Displayitem.transform);
            newItemDisplay.sprite = item.Test;
        }
    }

    private List<ItemSO> SelectRandomItems(int count)
    {
        List<ItemSO> allItems = i.Item;
        List<ItemSO> selectedItems = new List<ItemSO>();

        // 중복을 최소화하며 랜덤하게 아이템을 선택
        HashSet<int> chosenIndices = new HashSet<int>();

        while (chosenIndices.Count < count)
        {
            int randomIndex = Random.Range(0, allItems.Count);
            chosenIndices.Add(randomIndex);
        }

        foreach (int index in chosenIndices)
        {
            selectedItems.Add(allItems[index]);
        }

        return selectedItems;
    }
}

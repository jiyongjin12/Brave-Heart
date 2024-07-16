using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ChoiceItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryTesting Inventory;
    public ItemSO item;

    private Vector3 originalScale;
    private float scaleMultiplier = 1.17f;
    private float tweenTime = 0.2f;

    void Start()
    {
        originalScale = transform.localScale;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var Reward = RewardItem.instance;

        if (Inventory != null)
        {
            Inventory.ItemInfo.Add(item);
            Reward.ChoicePanelActiveFalse(); // Choice 패널 SetActive(false)하는 코드
            Debug.Log("CHECK");
            InventoryOnOff.inventoryOnOff.OnInventory(); // 인벤 켜지기
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * scaleMultiplier, tweenTime).SetEase(Ease.OutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, tweenTime).SetEase(Ease.OutSine);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RewardItemInformation : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemSO item;
    public InventoryTesting Inventory;
    public GameObject ii;

    private Vector3 originalScale;
    private float scaleMultiplier = 1.17f;
    private float tweenTime = 0.2f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Inventory != null)
        {
            Inventory.ItemInfo.Add(item);
            ii.SetActive(false);
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

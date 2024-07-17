using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ItemProp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryTesting Inventory;
    public ItemSO item;

    [SerializeField] public GameObject itemProp; // 아이템 받침대
    [SerializeField] public TMP_Text Goldtext;
    [SerializeField] public Image itemSprite; 
    public int Pride; //가격
 
    private Vector3 originalScale;
    private float scaleMultiplier = 1.17f;
    private float tweenTime = 0.2f;

    private void Start()
    {
        originalScale = transform.localScale;

        itemSprite.sprite = item.Test;
        Debug.Log(itemSprite.sprite);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory != null)
            {
                Inventory.ItemInfo.Add(item);
                Debug.Log("CHECK");
            }
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

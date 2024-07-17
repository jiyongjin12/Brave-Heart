using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ItemProp : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryTesting Inventory;
    public ItemSO item;

    [SerializeField] private GameObject blackScreen;
    [SerializeField] public GameObject itemProp; // 아이템 받침대
    [SerializeField] public TMP_Text Goldtext;
    [SerializeField] public Image itemSprite; 
    public int Pride; //가격
    public bool PurchasedItem = false;


    private Vector3 originalScale;
    private Quaternion originalRotation;
    private float scaleMultiplier = 1.17f;
    private float tweenTime = 0.2f;

    private void Start()
    {
        originalScale = transform.localScale;
        originalRotation = blackScreen.transform.rotation;

        itemSprite.sprite = item.Test;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameSuvManager.instance.Gold += 100;
            Debug.Log(GameSuvManager.instance.Gold);
        }

        UpdateBlackScreenVisibility();
    }

    private void UpdateBlackScreenVisibility()
    {
        blackScreen.SetActive(PurchasedItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!PurchasedItem && GameSuvManager.instance.Gold >= Pride)
            {
                if (Inventory != null)
                {
                    Inventory.ItemInfo.Add(item);
                    PurchasedItem = true;
                    //Debug.Log("CHECK");
                    UpdateBlackScreenVisibility();
                }
            }
            else
            {
                Debug.Log("No 머니 or 이미 구매함");
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

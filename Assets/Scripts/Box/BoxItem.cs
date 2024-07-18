using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class BoxItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemSO item;
    public Image itemImage;

    private Vector3 originalScale;
    private float scaleMultiplier = 1.17f;
    private float tweenTime = 0.2f;
    private BoxStage boxStage;

    public void Initialize(ItemSO newItem, BoxStage stage)
    {
        item = newItem;
        boxStage = stage;
        itemImage.sprite = item.Test;
        AdjustImageToSprite();
    }

    private void AdjustImageToSprite()
    {
        if (itemImage.sprite != null)
        {
            RectTransform rt = itemImage.GetComponent<RectTransform>();
            Vector2 originalSize = rt.sizeDelta;
            Vector2 spriteSize = new Vector2(itemImage.sprite.rect.width, itemImage.sprite.rect.height);

            float widthRatio = originalSize.x / spriteSize.x;
            float heightRatio = originalSize.y / spriteSize.y;
            float scaleFactor = Mathf.Min(widthRatio, heightRatio);

            Vector2 adjustedSize = spriteSize * scaleFactor;
            rt.sizeDelta = adjustedSize;
        }
    }

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            boxStage.OnBoxItemClick(item);
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

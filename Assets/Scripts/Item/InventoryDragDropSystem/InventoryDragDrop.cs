using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Inventory inventoryTetris;
    private PlacedObject placedObject;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        placedObject = GetComponent<PlacedObject>();
    }

    public void Setup(Inventory inventoryTetris)
    {
        this.inventoryTetris = inventoryTetris;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .7f;
        canvasGroup.blocksRaycasts = false;

        ItemSO.CreateVisualGrid(transform.GetChild(0), placedObject.GetPlacedObjectTypeSO() as ItemSO, inventoryTetris.GetGrid().GetCellSize());
        InventoryDragDropSystem.Instance.StartedDragging(inventoryTetris, placedObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        InventoryDragDropSystem.Instance.StoppedDragging(inventoryTetris, placedObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }
}

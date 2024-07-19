using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heal : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int heel;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            HealPlayer();
        }
    }

    private void HealPlayer()
    {
        GameManager.instance.hp += heel;

        // 오브젝트 삭제
        Destroy(gameObject);
    }
}

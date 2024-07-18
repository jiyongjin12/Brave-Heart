using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    [SerializeField]
    private bool OnOffCheck = true;

    public void OnNextButton()
    {
        var Reward = RewardItem.instance;

        if (Reward.GetItem)
        {
            Reward.ChoicePanelActiveFalse(); //Choice 패널 SetActive(false)하는 코드
        }
        else
        {
            FadeSystem.instance.ChangeScene("Map");
        }
    }

    public void ShopNextButton()
    {
        if (OnOffCheck == true && ShopCanvas.instance.PurchaseCheck == true)
        {
            OnOffCheck = false;
            InventoryOnOff.inventoryOnOff.OnInventory();
            Debug.Log("chek");
        }
        else
        {
            FadeSystem.instance.ChangeScene("Map");
        }
    }


    public void Next()
    {
        FadeSystem.instance.ChangeScene("Map");
    }
}

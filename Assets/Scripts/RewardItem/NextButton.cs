using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public void OnNextButton()
    {
        var Reward = RewardItem.instance;

        if (Reward.GetItem)
        {
            //Debug.Log("Check Destroy ChoicePanel");
            Reward.ChoicePanelActiveFalse(); //Choice 패널 SetActive(false)하는 코드
        }
        else
        {
            //Debug.Log("Check Next Scene");
            FadeSystem.instance.ChangeScene("Map");
        }
    }
}

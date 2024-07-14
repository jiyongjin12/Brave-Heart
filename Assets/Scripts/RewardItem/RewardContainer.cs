using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardContainer : MonoBehaviour
{
    [SerializeField] private Button rewardButton;
    [SerializeField] private Image rewardImage;
    [SerializeField] private TextMeshProUGUI rewardText;

    public Button RewardButton => rewardButton;

    public void BuildReward(Sprite rewardSprite, string rewardDescription)
    {
        rewardImage.sprite = rewardSprite;
        rewardText.text = rewardDescription;
    }
}

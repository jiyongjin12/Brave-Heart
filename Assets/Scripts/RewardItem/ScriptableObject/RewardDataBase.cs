using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDataBase : ScriptableObject
{
    [SerializeField] private Sprite rewardSprite;
    [TextArea] [SerializeField] private string rewardDescription;
    public Sprite RewardSprite => rewardSprite;
    public string RewardDescription => rewardDescription;
}

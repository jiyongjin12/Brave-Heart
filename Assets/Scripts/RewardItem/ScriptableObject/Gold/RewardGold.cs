using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward Gold", menuName = "RewardItem/Gold Reward", order = 4)]
public class RewardGold : RewardDataBase
{
    //[TextArea] [SerializeField] public string rewardDescription;
    //public Sprite goldImage;

    [SerializeField] private int maxGold;
    [SerializeField] private int minGold;
    public int MaxGold => maxGold;
    public int MinGold => minGold;
}

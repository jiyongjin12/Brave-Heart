using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Reward Container", menuName = "RewardItem/Reward", order = 4)]
public class RewardBox : ScriptableObject
{
    [SerializeField] private List<RewardGold> goldRewardDataList;
    [SerializeField] private List<ItemListings> itemRewardDataList;
    public List<RewardGold> GoldRewardDataList => goldRewardDataList;
    public List<ItemListings> ItemRewardDataList => itemRewardDataList;

    public int GetRandomGoldReward(out RewardGold rewardData)
    {
        rewardData = GoldRewardDataList.RandomItem();
        var value = Random.Range(rewardData.MinGold, rewardData.MaxGold);

        return value;
    }
}

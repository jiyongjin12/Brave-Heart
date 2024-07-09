using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward Container", menuName = "RewardItem/Reward", order = 4)]
public class RewardBox : ScriptableObject
{
    [SerializeField] private List<ItemListings> itemRewardDataList;
    [SerializeField] private List<RewardGold> goldRewardDataList;
    public List<ItemListings> ItemRewardDataList => itemRewardDataList;
    public List<RewardGold> GoldRewardDataList => goldRewardDataList;
}

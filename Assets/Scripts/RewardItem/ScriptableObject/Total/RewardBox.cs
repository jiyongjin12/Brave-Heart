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

    // ∞ÒµÂ
    public int GetRandomGoldReward(out RewardGold rewardData)
    {
        rewardData = GoldRewardDataList.RandomItem();
        var value = Random.Range(rewardData.MinGold, rewardData.MaxGold);

        return value;
    }

    // æ∆¿Ã≈€
    private int lastRandomIndex = -1;
    private int consecutiveCount = 0;

    public List<ItemSO> GetRandomCardRewardList(out ItemListings rewardData)
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, itemRewardDataList.Count);
            if (randomIndex == lastRandomIndex)
            {
                consecutiveCount++;
            }
            else
            {
                consecutiveCount = 0;
            }
        } while (consecutiveCount >= 2);

        lastRandomIndex = randomIndex;
        rewardData = itemRewardDataList[randomIndex];

        List<ItemSO> cardList = new List<ItemSO>();
        foreach (var cardData in rewardData.Item)
        {
            cardList.Add(cardData);
        }

        return cardList;

        //rewardData = ItemRewardDataList.RandomItem();

        //List<ItemSO> cardList = new List<ItemSO>();

        //foreach (var cardData in rewardData.Item)
        //    cardList.Add(cardData);

        //return cardList;
    }
}

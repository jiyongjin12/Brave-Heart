using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward Item", menuName = "RewardItem/Item Reward", order = 4)]
public class ItemListings : ScriptableObject
{
    [TextArea] [SerializeField] public string rewardDescription;

    [SerializeField] public List<ItemSO> Item;
}

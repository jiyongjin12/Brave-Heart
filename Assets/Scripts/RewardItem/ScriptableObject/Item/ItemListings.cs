using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reward Item", menuName = "RewardItem/Item Reward", order = 4)]
public class ItemListings : RewardDataBase
{
    //[TextArea] [SerializeField] public string rewardDescription;
    //public Sprite itemImage;

    [SerializeField] public List<ItemSO> Item;
}

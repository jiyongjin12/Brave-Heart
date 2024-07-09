using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private GameObject CombatRewardPanel;
    [SerializeField] private GameObject Reward;
    [SerializeField] private GameObject[] BoxPrefab;
    [SerializeField] private GameObject ItemBox;

    public RewardGold _Gold;
    public RewardItem _Item;


    public void OnGoldBox()
    {
        
    }

    public void OnItemBox()
    {
        ItemBox.SetActive(true);
    }
}

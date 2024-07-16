using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RewardItem : MonoBehaviour
{
    public static RewardItem instance { get; private set; }
    [SerializeField] private RewardBox boxInformation; // 스크립터블 오브젝트 (Text 와 골드 정보,  Text와 아이템 리스트)
    [Header("보상 선택창")]
    [SerializeField] private GameObject combatRewardPanel; // 보상 선택창
    [SerializeField] private Transform RewardRoot; // 소환 위치  
    [SerializeField] private GameObject SpawnBox; // 소환 프리펩
    [SerializeField] private RewardContainer rewardContainer; // 소환할 오브젝트의 스크립트
    [Header("아이템 선택창")]
    [SerializeField] private GameObject ChoicePanel; // 아이템 선택창
    [SerializeField] private ChoiceItem ChoiceItemPrefab;// 소환 프리펩 
    [SerializeField] private Transform itemSpawnRoot; // 소환 위치

    [SerializeField] private int SpawnItemNum = 4;

    private readonly List<RewardContainer> _currentRewardsList = new List<RewardContainer>();
    private readonly List<ItemSO> _ItemRewardList = new List<ItemSO>();

    public enum RewardType
    {
        Gold,
        Item
    }


    private void Awake()
    {
        instance = this;
    }

    private void Update() // 실행테스트용
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            WaveWin();
        }
    }

    public void WaveWin()
    {
        combatRewardPanel.SetActive(true);

        //소환할 것들 넣기
        BuildReward(RewardType.Gold);
        BuildReward(RewardType.Item);
    }

    public void BuildReward(RewardType rewrdType)
    {
        var rewardClone = Instantiate(rewardContainer, RewardRoot);
        _currentRewardsList.Add(rewardClone);

        switch (rewrdType)
        {
            case RewardType.Gold:
                var rewardGold = boxInformation.GetRandomGoldReward(out var goldRewardData);
                rewardClone.BuildReward(goldRewardData.RewardSprite, goldRewardData.RewardDescription);
                rewardClone.RewardButton.onClick.AddListener(() => GetGoldReward(rewardClone, rewardGold));
                break;
            case RewardType.Item:
                var rewardItemList = boxInformation.GetRandomCardRewardList(out var ItemRewardData);
                _ItemRewardList.Clear();
                foreach (var itemData in rewardItemList)
                    _ItemRewardList.Add(itemData);
                rewardClone.BuildReward(ItemRewardData.RewardSprite, ItemRewardData.RewardDescription);
                rewardClone.RewardButton.onClick.AddListener(() => GetItemReward(SpawnItemNum));
                break;
        }

    }

    private void GetGoldReward(RewardContainer rewardContainer, int amount)
    {
        GameSuvManager.instance.gameData.Gold += amount;
        _currentRewardsList.Remove(rewardContainer);
        //골드 텍스트 업데이트 여기에 작성하면 됨

        Destroy(rewardContainer.gameObject);
    }

    private void GetItemReward(int num)
    {
        ChoicePanel.SetActive(true);

        for (int i = 0; i < num; i++)
        {
            if (_ItemRewardList.Count > 0)
            {
                var randomIndex = Random.Range(0, _ItemRewardList.Count);
                var itemData = _ItemRewardList[randomIndex];

                var choiceItemClone = Instantiate(ChoiceItemPrefab, itemSpawnRoot);
                choiceItemClone.item = itemData;
            }
        }
    }

















    //public static RewardItem instance { get; private set; }
    //[SerializeField] private GameObject combatRewardPanel;
    //[SerializeField] private RewardBox boxInformation; // 스크립터블 오브젝트 (Text 와 골드 정보,  Text와 아이템 리스트)
    //[SerializeField] private GameObject RewardRoot; // 소환 위치  
    //[SerializeField] private GameObject SpawnBox; // 소환할 형태 
    //[SerializeField] private RewardContainer rewardContainer; // 소환할 오브젝트의 스크립트

    //private void Awake()
    //{
    //    instance = this;
    //}

    //private void Update() // 실행테스트용
    //{
    //    if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        WaveWin();
    //    }
    //}

    //public void WaveWin()
    //{
    //    combatRewardPanel.SetActive(true);

    //    //소환할 것들 넣기
    //    GetGoldReward();
    //    GetItemReward();
    //}

    //private void GetGoldReward()
    //{
    //    foreach (var goldReward in boxInformation.GoldRewardDataList)
    //    {
    //        GameObject spawnedBox = Instantiate(SpawnBox, RewardRoot.transform);
    //        RewardContainer rewardContainer = spawnedBox.GetComponent<RewardContainer>();
    //        if (rewardContainer != null)
    //        {
    //            rewardContainer.BuildReward(goldReward.goldImage, goldReward.rewardDescription);
    //        }
    //    }
    //}

    //private void GetItemReward()
    //{
    //    foreach (var itemReward in boxInformation.ItemRewardDataList)
    //    {
    //        GameObject spawnedBox = Instantiate(SpawnBox, RewardRoot.transform);
    //        RewardContainer rewardContainer = spawnedBox.GetComponent<RewardContainer>();
    //        if (rewardContainer != null)
    //        {
    //            rewardContainer.BuildReward(itemReward.itemImage, itemReward.rewardDescription);
    //        }
    //    }
    //}

    ////public void OnGoldBox(GameObject button)
    ////{
    ////    if (_Gold != null)
    ////    {
    ////        int randomGold = Random.Range(_Gold.MinGold, _Gold.MaxGold + 1);
    ////        Gold += randomGold;
    ////        Debug.Log("Gold added: " + randomGold + ", Total Gold: " + Gold);
    ////    }

    ////    Destroy(button);
    ////}

}

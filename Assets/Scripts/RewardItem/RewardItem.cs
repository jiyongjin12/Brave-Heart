using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RewardItem : MonoBehaviour
{
    public static RewardItem instance { get; private set; }
    [SerializeField] private GameObject combatRewardPanel;
    [SerializeField] private RewardBox boxInformation; // 스크립터블 오브젝트 (Text 와 골드 정보,  Text와 아이템 리스트)
    [SerializeField] private Transform RewardRoot; // 소환 위치  
    [SerializeField] private GameObject SpawnBox; // 소환할 형태 
    [SerializeField] private RewardContainer rewardContainer; // 소환할 오브젝트의 스크립트

    private readonly List<RewardContainer> _currentRewardsList = new List<RewardContainer>();

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

        //GetGoldReward();
        //GetItemReward();
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
        }

    }


    private void GetGoldReward()
    {
        foreach (var goldReward in boxInformation.GoldRewardDataList)
        {
            GameObject spawnedBox = Instantiate(SpawnBox, RewardRoot);
            RewardContainer rewardContainer = spawnedBox.GetComponent<RewardContainer>();
            if (rewardContainer != null)
            {
                rewardContainer.BuildReward(goldReward.RewardSprite, goldReward.RewardDescription);
            }
        }
    }

    private void GetItemReward()
    {
        foreach (var itemReward in boxInformation.ItemRewardDataList)
        {
            GameObject spawnedBox = Instantiate(SpawnBox, RewardRoot);
            RewardContainer rewardContainer = spawnedBox.GetComponent<RewardContainer>();
            if (rewardContainer != null)
            {
                rewardContainer.BuildReward(itemReward.RewardSprite, itemReward.RewardDescription);
            }
        }
    }

    private void GetGoldReward(RewardContainer rewardContainer, int amount)
    {
        GameSuvManager.instance.Gold += amount;
        _currentRewardsList.Remove(rewardContainer);
        //골드 텍스트 업데이트 여기에 작성하면 됨

        Destroy(rewardContainer.gameObject);
    }

    //public void OnGoldBox(GameObject button)
    //{
    //    if (_Gold != null)
    //    {
    //        int randomGold = Random.Range(_Gold.MinGold, _Gold.MaxGold + 1);
    //        Gold += randomGold;
    //        Debug.Log("Gold added: " + randomGold + ", Total Gold: " + Gold);
    //    }

    //    Destroy(button);
    //}


















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








    //============================================================================================================







    //public static RewardItem instance { get; private set; }
    //[SerializeField] private GameObject CombatRewardPanel;
    //[SerializeField] private GameObject Reward;
    ////[SerializeField] private GameObject[] BoxPrefab;
    //[SerializeField] private GameObject ItemBox;

    //public RewardGold _Gold;
    //public ItemListings _Item;

    //public int Gold;

    //private void Awake()
    //{
    //    instance = this;

    //}



    //public void WaveWin()
    //{
    //    CombatRewardPanel.SetActive(true); 

    //    //foreach (GameObject prefab in BoxPrefab)  
    //    //{
    //    //    if (prefab != null)
    //    //    {
    //    //        GameObject instance = Instantiate(prefab, Reward.transform);
    //    //        Debug.Log("Instantiated " + prefab.name + " as child of Reward");
    //    //    }
    //    //}
    //}


    //public void OnGoldBox(GameObject button)
    //{
    //    if (_Gold != null)
    //    {
    //        int randomGold = Random.Range(_Gold.MinGold, _Gold.MaxGold + 1);
    //        Gold += randomGold;
    //        Debug.Log("Gold added: " + randomGold + ", Total Gold: " + Gold);
    //    }

    //    Destroy(button);
    //}

    //public void OnItemBox(GameObject button)
    //{
    //    ItemBox.SetActive(true);

    //    Destroy(button);
    //}

    //public void NextButton()
    //{
    //    CombatRewardPanel.SetActive(false);
    //    SceneManager.LoadScene("Map");
    //}
}

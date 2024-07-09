using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private GameObject CombatRewardPanel;
    [SerializeField] private GameObject Reward;
    //[SerializeField] private GameObject[] BoxPrefab;
    [SerializeField] private GameObject ItemBox;

    public RewardGold _Gold;
    public ItemListings _Item;

    public int Gold;

    private void Start()
    {
        WaveWin();
    }

    public void WaveWin()
    {
        CombatRewardPanel.SetActive(true);

        //foreach (GameObject prefab in BoxPrefab)  
        //{
        //    if (prefab != null)
        //    {
        //        GameObject instance = Instantiate(prefab, Reward.transform);
        //        Debug.Log("Instantiated " + prefab.name + " as child of Reward");
        //    }
        //}
    }


    public void OnGoldBox(GameObject button)
    {
        if (_Gold != null)
        {
            int randomGold = Random.Range(_Gold.MinGold, _Gold.MaxGold + 1);
            Gold += randomGold;
            Debug.Log("Gold added: " + randomGold + ", Total Gold: " + Gold);
        }

        Destroy(button);
    }

    public void OnItemBox(GameObject button)
    {
        ItemBox.SetActive(true);

        Destroy(button);
    }

    public void NextButton()
    {
        CombatRewardPanel.SetActive(false);
    }
}

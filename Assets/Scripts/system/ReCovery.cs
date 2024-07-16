using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReCovery : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform playerBattleTrans;

    private void Awake()
    {
        Instantiate(PlayerPrefab, playerBattleTrans);
        GameSuvManager.instance.gameData.playerHP += 10;
    }

    private void Update()
    {
        if(GameSuvManager.instance.gameData.playerHP >= GameSuvManager.instance.gameData.playerMaxHP)
        {
            GameSuvManager.instance.gameData.playerHP = GameSuvManager.instance.gameData.playerMaxHP;
        }
    }

    public void Next()
    {
        SceneManager.LoadScene("Map");
    }
}

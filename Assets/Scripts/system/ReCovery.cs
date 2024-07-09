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
        GameSuvManager.instance.playerHP += 10;
    }

    private void Update()
    {
        if(GameSuvManager.instance.playerHP >= GameSuvManager.instance.playerMaxHP)
        {
            GameSuvManager.instance.playerHP = GameSuvManager.instance.playerMaxHP;
        }
    }

    public void Next()
    {
        SceneManager.LoadScene("Map");
    }
}

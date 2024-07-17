using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject No;
    [SerializeField] private GameObject Audio;
    //public TextEffect TextTitle;

    public Vector2 newSize = new Vector2(200, 100);
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void AudioTrue()
    {
        Audio.SetActive(true);
    }
    public void AudioFalse()
    {
        Audio.SetActive(false);
    }


    public void NewGame()
    {
        GameSuvManager.instance.gameData.playerMaxHP = 100;
        GameSuvManager.instance.gameData.playerHP = 100;
        GameSuvManager.instance.gameData.playerDmg = 0;
        GameSuvManager.instance.gameData.stage = 0;
        GameSuvManager.instance.gameData.HPUp = 1f;
        GameSuvManager.instance.gameData.DamageUP = 1f;
        GameSuvManager.instance.gameData.playerCounter = 50f;
        GameSuvManager.instance.gameData.Gold = 0;
        GameSuvManager.instance.gameData.stageNum = 0;

        FadeSystem.instance.ChangeScene("Map");
    }

    public void LoadGame()
    {
        switch (GameSuvManager.instance.gameData.stageNum)
        {
            case 0:
                FadeSystem.instance.ChangeScene("Map");
                break;
            case 1:
                FadeSystem.instance.ChangeScene("SampleScene");
                break;
            case 2:
                FadeSystem.instance.ChangeScene("EliteScene");
                break;
            case 3:
                FadeSystem.instance.ChangeScene("RecoveryScene");
                break;
            case 4:
                FadeSystem.instance.ChangeScene("Map");
                break;
            case 5:
                FadeSystem.instance.ChangeScene("Map");
                break;
            case 6:
                FadeSystem.instance.ChangeScene("BossScene");
                break;
            default:
                FadeSystem.instance.ChangeIntScene(GameSuvManager.instance.Rand());
                break;
        }
        //No.SetActive(true);
    }

    public void PointerEnter()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("InGame");
    }
}

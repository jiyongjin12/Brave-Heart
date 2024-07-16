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

        FadeSystem.instance.ChangeScene("Map");
    }

    public void LoadGame()
    {
        No.SetActive(true);
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

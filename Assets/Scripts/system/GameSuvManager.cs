using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData 
{
    public static GameData instance { get; private set; }

    public float BGMSound = 0;
    public float EffectSound = 0;

    public float stage = 0;
    public float stageNum = 100;
    public float HPUp;
    public float DamageUP;

    public float playerHP;
    public float playerMaxHP;
    public float playerDmg;
    public float playerCounter;

    public int Gold;
}

public class GameSuvManager : MonoBehaviour
{
    public string GameDataFileName = ".json";

    public int Gold;
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static GameSuvManager _instance;
    public static GameSuvManager instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "GameSuvManager";
                _instance = _container.AddComponent(typeof(GameSuvManager)) as GameSuvManager;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
            gameData.stageNum = 2;
        else if (SceneManager.GetActiveScene().name == "EliteScene")
            gameData.stageNum = 3;
        else if (SceneManager.GetActiveScene().name == "RecoveryScene")
            gameData.stageNum = 4;
        else if (SceneManager.GetActiveScene().name == "EliteScene")
            gameData.stageNum = 5;
        else if (SceneManager.GetActiveScene().name == "ShopStage")
            gameData.stageNum = 6;
        else if (SceneManager.GetActiveScene().name == "BossScene")
            gameData.stageNum = 7;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != gameData.stageNum)
            LoadScene();
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    FadeSystem.instance.ChangeScene("BossScene");
        //}
    }

    public int Rand()
    {
        int rand = Random.Range(2, 5);

        return rand;
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공");
            string FormJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FormJsonData);
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
        }
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }
}

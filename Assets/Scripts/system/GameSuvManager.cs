using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSuvManager : MonoBehaviour
{
    public float stage;
    public float playerHP;
    public float playerMaxHP;
    public int Gold;

    public static GameSuvManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int Rand()
    {
        int rand = Random.Range(2, 5);

        return rand;
    }
}

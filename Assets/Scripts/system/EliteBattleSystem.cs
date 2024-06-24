using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteBattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public EnemyPer[] enemyPrefab;

    public Transform playerBattleTrans; //플레이어의 좌표
    public Enemy[] enemySlot;

    public int enemyCount; //0이 되면 플레이어 승리
    public int curEnemy; //생성될 에너미 수

    public int number = 0;

    public GameObject shieldIcon;

    public GameObject enemy;

    public float battleMotion = 0;
    private float acc;

    public enum State
    {
        start, playerTurn, enemyTurn, win, loss
    }

    public State state;
    private void Awake()
    {
        Instantiate(PlayerPrefab, playerBattleTrans);
        //instance = this;
        state = State.start;
        shieldIcon.SetActive(false);
        EnemyPercent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPercent()
    {
        acc = 0;
        foreach (var item in enemyPrefab)
        {
            acc += item.Chance;
            item.Weight = acc;
        }
    }

    private void SpawnEnemy(Vector2 pos)
    {
        var clone = enemyPrefab[GetRandom()];

        enemy = Instantiate(clone.Prefab, pos, Quaternion.identity);
        Enemy.instance.EnemyPos(clone.Prefab.GetComponent<Enemy>());
        enemy.transform.position = new Vector3(pos.x, Enemy.instance.yPos);
        enemySlot[number] = enemy.GetComponent<Enemy>();
        GameManager.instance.SpawnEnemyHPSlider(enemySlot[number]);
    }

    private int GetRandom()
    {
        float random = Random.value * acc;

        for (int i = 0; i < enemyPrefab.Length; ++i)
        {
            if (enemyPrefab[i].Weight >= random)
            {
                return i;
            }
        }

        return 0;
    }
}

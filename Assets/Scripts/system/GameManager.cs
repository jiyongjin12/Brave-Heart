using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float playerDamage;

    public float hp;
    public float Maxhp;

    public bool isNew;
    private int number;

    public List<Enemy> enemySlot = new List<Enemy>(); //적 4명까지 저장

    Enemy enemy;

    private void Awake()
    {
        isNew = false;
        number = 0;
        instance = this;
        hp = Maxhp;
    }

    private void Update()
    {
        DeadEnmey();
    }

    void DeadEnmey()
    {
        if (enemySlot[0].hp > 0)
            return;

        enemySlot.RemoveAt(0);
        //for (int i = 1; i < enemySlot.Count; i++)
        //{
        //    enemySlot[i - 1] = enemySlot[i];
        //}
    }

    void NewEnemy()
    {
        if (!isNew)
            return;

        //enemySlot[number] = BattleSystem
    }
}

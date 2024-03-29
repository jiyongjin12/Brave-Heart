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

    Enemy enemy;

    private void Awake()
    {
        isNew = false;
        number = 0;
        instance = this;
        hp = Maxhp;
    }

    public void DeadEnmey()
    {

        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            if (i == BattleSystem.instance.enemySlot.Length - 1)
                BattleSystem.instance.enemySlot[i] = null;

            BattleSystem.instance.enemySlot[i - 1] = BattleSystem.instance.enemySlot[i];

        }
    }

    void NewEnemy()
    {
        if (!isNew)
            return;

        //enemySlot[number] = BattleSystem
    }
}

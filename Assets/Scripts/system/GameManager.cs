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
    public int number;

    private int emptyEnemy;

    private void Awake()
    {
        isNew = false;
        number = 0;
        emptyEnemy = 1;
        instance = this;
        hp = Maxhp;
    }

    public void DeadEnmey()
    {

        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {

            if (BattleSystem.instance.enemySlot[i] == null)
            {
                emptyEnemy++;
                continue;
            }

            BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i];

            if (i == BattleSystem.instance.enemySlot.Length - emptyEnemy)
            {
                BattleSystem.instance.enemySlot[i] = null;
                emptyEnemy = 1;
            }
        }
        emptyEnemy = 1;
    }

    void NewEnemy()
    {
        if (!isNew)
            return;

        //enemySlot[number] = BattleSystem
    }
}

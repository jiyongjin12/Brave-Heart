using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float playerDamage;

    public float hp;
    public float Maxhp;

    [SerializeField] private Image _hpBar;

    public float shield;
    public float counter;

    public bool counterAttack = false;

    private int emptyEnemy;

    private void Awake()
    {
        emptyEnemy = 1;
        instance = this;
        hp = Maxhp;
        counterAttack = false;
    }

    private void Update()
    {
        _hpBar.fillAmount =  hp / Maxhp;
    }

    public void DeadEnmey()
    {
        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            if (BattleSystem.instance.enemySlot[i - emptyEnemy] == null)
            {
                BattleSystem.instance.number--;
                emptyEnemy++;
                break;
            }
            else BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i];

            if (i == BattleSystem.instance.enemySlot.Length - 1)
            {
                BattleSystem.instance.enemySlot[i] = null;
                emptyEnemy = 1;
            }
        }
        emptyEnemy = 1;
    }
}

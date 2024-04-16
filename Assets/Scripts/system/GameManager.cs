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
    [SerializeField] private Text TextDefense;

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
        if(shield > 0)
        {
            TextDefense.text = shield.ToString();
        }
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
            else if (BattleSystem.instance.enemySlot[i] == null)
            {
                BattleSystem.instance.number--;
                emptyEnemy++;
                break;
            }
            else BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i];

            if (i == BattleSystem.instance.enemySlot.Length - emptyEnemy)
            {
                BattleSystem.instance.enemySlot[i] = null;
            }
        }
        emptyEnemy = 1;
    }
}

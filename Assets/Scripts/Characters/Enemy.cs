using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public enum EnemyType { Warrior = 0, Archer = 1, Wizard = 2, Boss }
    public enum EnemyKind { Slime, Orc, goblin, Boss }
    public EnemyType enemyType;
    public EnemyKind Enemykind;
    public int Level;

    public float hp;
    public float damage;
    public bool isMovement = false;

    private bool deadEnemy = false;

    [SerializeField]
    private int Charge;

    public static Enemy instance { get; private set; }

    private void Awake()
    {
        Charge = 0;
        hp = enemyData.baseHp + enemyData.maxHp[Level];
        damage = enemyData.baseDamage + enemyData.damages[Level];
        RandomType();
        instance = this;
    }

    private void Update()
    {
        if(hp <= 0) StartCoroutine(Dead());
        if (deadEnemy == true)
            deadEnemy = false;
    }

    IEnumerator Dead()
    {
        deadEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        if(BattleSystem.instance.state == BattleSystem.State.playerTurn) GameManager.instance.DeadEnmey();
        if(BattleSystem.instance.state == BattleSystem.State.enemyTurn) BattleSystem.instance.number--;
        BattleSystem.instance.enemyCount--;
    }

    public void AttackArcher(int i)
    {

        BattleSystem.instance.EnemyAttack(i);
    }

    public void AttackWizard(int i)
    {

        BattleSystem.instance.enemySlot[i].Charge++;
        if(BattleSystem.instance.enemySlot[i].Charge >= 3)
        {
            BattleSystem.instance.EnemyAttack(i);
            BattleSystem.instance.enemySlot[i].Charge = 0;
        }
        
    }

    void RandomType()
    {
        int rand_Type = Random.Range(0, 3);

        switch (rand_Type)
        {
            case (int)EnemyType.Warrior:
                this.enemyType = EnemyType.Warrior;
                break;
            case (int)EnemyType.Archer:
                this.enemyType = EnemyType.Archer;
                break;
            case (int)EnemyType.Wizard:
                this.enemyType = EnemyType.Wizard;
                break;
        }
        if (this.Enemykind == EnemyKind.Orc) this.enemyType = EnemyType.Warrior;

        EnemyTypeData();
    }

    void EnemyTypeData()
    {
        switch (enemyType)
        {
            case EnemyType.Warrior:
                hp = enemyData.baseHp + enemyData.maxHp[Level];
                damage = enemyData.baseDamage + enemyData.damages[Level];
                break;
            case EnemyType.Archer:
                hp = enemyData.baseHp / 2 + enemyData.maxHp[Level];
                damage = enemyData.baseDamage + enemyData.damages[Level];
                break;
            case EnemyType.Wizard:
                hp = enemyData.baseHp / 2 + enemyData.maxHp[Level] / 2;
                damage = enemyData.baseDamage + enemyData.Wizarddmg[Level] * 2;
                break;
        }
    }

    public void LevelUp()
    {
        Level++;
    }
}

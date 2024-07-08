using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public enum EnemyType { Warrior = 0, Archer = 1, Wizard = 2, Elite = 3, Boss }
    public enum EnemyKind { Slime, Orc, Goblin, Boss }
    public EnemyType enemyType;
    public EnemyKind Enemykind;
    public int Level;

    public float hp;
    public float damage;

    public float Maxhp;
    //public bool isMovement = false;

    private bool deadEnemy = false;

    public float yPos;

    public int Charge;

    private float shakeTime = 4f;
    private float BeforeHp;

    public int enemyNum;
    public bool MouseClick;

    public bool isAttacking = false;

    public int MonsterAttackNum;

    public static Enemy instance { get; private set; }

    private void Awake()
    {
        this.enemyNum = (BattleSystem.instance.curEnemy - BattleSystem.instance.minusNum) * -1;
        Charge = 0;
        hp = enemyData.baseHp + enemyData.maxHp[Level];
        Maxhp = enemyData.baseHp + enemyData.maxHp[Level];
        damage = enemyData.baseDamage + enemyData.damages[Level];
        //this.BeforeHp = this.hp;
        instance = this;
    }

    private void Update()
    {
        if (hp <= 0 && !Unit.instance.isAttacking && deadEnemy == false)
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        deadEnemy = true;
        //BattleSystem.instance.EliteDead = true;
        ReEnemyNum();
        BattleSystem.instance.TNum = 0;
        yield return YieldCache.WaitForSeconds(1f);
        Destroy(gameObject);
        BattleSystem.instance.number--;
        BattleSystem.instance.enemyCount--;
        deadEnemy = false;
    }

    public void ReEnemyNum()
    {
        BattleSystem.instance.minusNum -= 1;
        for (int i = BattleSystem.instance.TNum + 1; i < BattleSystem.instance.number; i++)
        {
            BattleSystem.instance.enemySlot[i].enemyNum -= 1;
        }
    }

    public IEnumerator ShakeMonster(int i)
    {
        float shakePower;
        //if (BattleSystem.instance.state == BattleSystem.State.playerTurn)
        //{
        //    shakeTime = 4f;
        //    shakePower = 0.01f;
        //}
        //else
        //{
        //    shakeTime = 2f;
        //    shakePower = 0.05f;
        //}
        shakeTime = 2f;
        shakePower = 0.2f;

        Vector3 origin = BattleSystem.instance.enemySlot[i].transform.position;

        while (shakeTime >= 0f)
        {
            //if (BattleSystem.instance.state == BattleSystem.State.playerTurn) shakeTime -= 0.003f;
            //else shakeTime -= 0.05f;
            shakeTime -= 0.05f;
            BattleSystem.instance.enemySlot[i].transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * shakeTime;
            yield return null;
        }

        BattleSystem.instance.enemySlot[i].transform.position = origin;
    }

    public void EnemyPos(Enemy Enemy)
    {
        if (Enemy.Enemykind == EnemyKind.Goblin || Enemy.Enemykind == EnemyKind.Slime) yPos = 5;
        else if (Enemy.Enemykind == EnemyKind.Orc) yPos = 5.25f;
    }

    public void AttackArcher(int i)
    {
        isAttacking = true;
        MonsterAttackNum = i;
        GameManager.instance.EnemyAttack(i);
    }

    public void AttackWizard(int i)
    {
        isAttacking = true;
        MonsterAttackNum = i;
        BattleSystem.instance.enemySlot[i].Charge++;
        if(BattleSystem.instance.enemySlot[i].Charge >= 3)
        {
            GameManager.instance.EnemyAttack(i);
            BattleSystem.instance.enemySlot[i].Charge = 0;
        }
        
    }

    public void AttackElite(int i)
    {
        isAttacking = true;
        MonsterAttackNum = i;
        GameManager.instance.EnemyAttack(i);
    }

    private void HitText()
    {
        Instantiate(GameManager.instance.Damage, this.transform.position, Quaternion.identity);
    }

    public void LevelUp()
    {
        Level++;
    }
}

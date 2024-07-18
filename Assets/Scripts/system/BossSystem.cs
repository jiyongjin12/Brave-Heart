using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    private Vector3 newEnemyPos = new Vector3(8, 4f);
    private float charge = 0;
    private float originDamage;

    
    public static BossSystem instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void BossTurn()
    {
        originDamage = BattleSystem.instance.enemySlot[0].damage;
        if (BattleSystem.instance.enemySlot[0].hp >= BattleSystem.instance.enemySlot[0].Maxhp / 2)
            StartCoroutine(FirstPage());
        else
            StartCoroutine(SecondPage());
    }

    IEnumerator FirstPage()
    {
        int rand;
        if (BattleSystem.instance.enemySlot[8] != null)
            rand = 1;
        else
            rand = 0;

        yield return null;
        switch (rand)
        {
            case 0:
                FirstBossSkill();
                break;
            case 1:
                NullBossSkill();
                break;
        }
    }

    IEnumerator SecondPage()
    {
        int rand;
        if (BattleSystem.instance.enemySlot[8] != null)
            rand = Random.Range(1, 3);
        else if (charge >= 1)
            rand = 2;
        else
            rand = Random.Range(0, 3);
        switch (rand)
        {
            case 0:
                FirstBossSkill();
                break;
            case 1:
                StartCoroutine(SecondBossSkill());
                break;
            case 2:
                StartCoroutine(ThirdBossSkill());
                break;
        }
        yield return null;
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public void NullBossSkill()
    {
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public void FirstBossSkill()
    {
        BattleSystem.instance.curEnemy++;
        BattleSystem.instance.minusNum++;
        BattleSystem.instance.SpawnEnemy(newEnemyPos);
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public IEnumerator SecondBossSkill()
    {
        BossAttack();
        yield return YieldCache.WaitForSeconds(0.5f);
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public IEnumerator ThirdBossSkill()
    {
        if(charge < 2)
        {
            charge++;
        }
        else
        {
            charge = 0;
            BattleSystem.instance.enemySlot[0].damage *= 1.5f;
            BossAttack();
        }
        yield return YieldCache.WaitForSeconds(0.5f);
        BattleSystem.instance.state = BattleSystem.State.playerTurn;
    }

    public void BossAttack()
    {
        if (GameManager.instance.counterAttack == true) Counter();
        else if (GameManager.instance.shield > 0)
        {
            GameManager.instance.shield -= BattleSystem.instance.enemySlot[0].damage;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[0].damage);
            GameManager.instance.TextDamage(BattleSystem.instance.playerBattleTrans);
            ShieldBreak();
        }
        else
        {
            GameManager.instance.hp -= BattleSystem.instance.enemySlot[0].damage;
            StartCoroutine(Unit.instance.ShakePlayer());
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[0].damage);
            GameManager.instance.TextDamage(BattleSystem.instance.playerBattleTrans);
        }
        BattleSystem.instance.enemySlot[0].damage = originDamage;
    }

    public void Counter()
    {
        float Success = GameManager.instance.GetRandom();
        if (Success == 0)
        {
            if (GameManager.instance.shield > 0)
            {
                GameManager.instance.shield -= BattleSystem.instance.enemySlot[0].damage * 2;
                ShieldBreak();
            }
            else
            {
                GameManager.instance.hp -= BattleSystem.instance.enemySlot[0].damage * 2;
                StartCoroutine(Unit.instance.ShakePlayer());
            }
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[0].damage * 2);
            GameManager.instance.TextDamage(BattleSystem.instance.playerBattleTrans);
        }
        else
        {
            StartCoroutine(Enemy.instance.ShakeBoss());
            BattleSystem.instance.enemySlot[0].hp -= GameManager.instance.playerDamage * 1.5f;
            Unit.instance.TextEnemyDamage(GameManager.instance.playerDamage * 1.5f);
            GameManager.instance.SpawnDamageText(BattleSystem.instance.enemySlot[0]);
        }
    }

    public void ShieldBreak()
    {
        if (GameManager.instance.shield <= 0)
        {
            BattleSystem.instance.shieldIcon.SetActive(false);
            StartCoroutine(Unit.instance.ShakePlayer());
            GameManager.instance.hp += GameManager.instance.shield;
        }
    }
}

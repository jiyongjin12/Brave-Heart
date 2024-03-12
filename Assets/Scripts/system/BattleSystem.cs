using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleTrans; //플레이어의 좌표
    public Transform[] enemyBattleTrans; //에너미 좌표

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    private int enemyCount; //0이 되면 플레이어 승리
    private int curEnemy; //생성될 에너미 수

    public BattleState state;

    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleTrans);
        playerGo.GetComponent<Unit>();

        for(int i = 0; i < enemyBattleTrans.Length-1; i++)
        {
            GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleTrans[i]);
            enemyGo.GetComponent<Unit>();
            curEnemy--;
        }

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        Debug.Log("1");
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHp(enemyUnit.curHp);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            enemyCount--;
            
        }
        if(enemyCount == 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHp(playerUnit.curHp);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            //승리했을때
        }
        if (state == BattleState.LOST)
        {
            //졌을때
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
}

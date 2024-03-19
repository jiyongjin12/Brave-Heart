using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject[] enemyPrefab;

    public static BattleSystem instance { get; private set; }
    public Transform playerBattleTrans; //플레이어의 좌표
    public Transform[] enemyBattleTrans; //에너미 좌표

    public int enemyCount; //0이 되면 플레이어 승리
    public int curEnemy; //생성될 에너미 수
    public enum State
    {
        start, playerTurn, enemyTurn, win, loss
    }

    public State state;

    private void Awake()
    {
        Instantiate(PlayerPrefab, playerBattleTrans);
        instance = this;
        state = State.start;
        BattleStart();
    }

    private void BattleStart()
    {
        for (int i = 0; i < enemyBattleTrans.Length - 1; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab[Random.Range(0, 3)], enemyBattleTrans[i]);
            GameManager.instance.enemySlot[i] = enemy.GetComponentInChildren<Enemy>();
        }
        state = State.playerTurn;
    }

    public void PlayerAttackButton()
    {
        if (state != State.playerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);


        if (GameManager.instance.enemySlot[0].hp > 0)
            GameManager.instance.enemySlot[0].hp -= GameManager.instance.playerDamage;

        if (enemyCount == 0)
        {
            state = State.win;
            BatleWin();
        }
        else
        {
            Debug.Log("적 턴");
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        if (GameManager.instance.enemySlot[0].transform.position.x != -2)
            for (int i = 0; i < GameManager.instance.enemySlot.Count; i++)
            {
                //GameManager.instance.enemySlot[i].transform.position.x -= 2;
            }
        else
            GameManager.instance.hp -= GameManager.instance.enemySlot[0].damage;

        if (GameManager.instance.hp == 0)
        {
            state = State.loss;
            BatleWin();
        }
        else
        {
            Debug.Log("적 턴");
            state = State.playerTurn;
        }
    }

    void BatleWin()
    {
        Debug.Log("게임 승리");
    }
}

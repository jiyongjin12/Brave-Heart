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
    public Enemy[] enemySlot;

    public int enemyCount; //0이 되면 플레이어 승리
    public int curEnemy; //생성될 에너미 수

    public GameObject enemy;

    public int number;
    private Vector3 attackLine = new Vector3(-4, 3);
    private bool click;

    public enum State
    {
        start, playerTurn, enemyTurn, win, loss
    }

    public State state;

    private void Awake()
    {
        Instantiate(PlayerPrefab, playerBattleTrans);
        instance = this;
        number = 0;
        click = false;
        state = State.start;
        BattleStart();
    }

    private void BattleStart()
    {
        for (int i = 0; i < 3; i++)
        {
            enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], new Vector3(4 + i * 2, 3), Quaternion.identity);
            enemySlot[number] = enemy.GetComponent<Enemy>();
            number++;
            curEnemy--;
        }
        state = State.playerTurn;
    }

    public void PlayerAttackButton()
    {
        if (state != State.playerTurn || click)
            return;

        click = true;
        StartCoroutine(PlayerAttack());
    }

    public void PlayerDefenseButton()
    {
        if (state != State.playerTurn || click)
            return;

        click = true;
        StartCoroutine(PlayerDefense());
    }

    public void PlayerCounterButton()
    {
        if (state != State.playerTurn || click)
            return;

        click = true;
        StartCoroutine(PlayerCounter());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);


        if (enemySlot[0].hp > 0) enemySlot[0].hp -= GameManager.instance.playerDamage;

        if (enemyCount == 0)
        {
            state = State.win;
            BatleEnd();
        }
        else
        {
            Debug.Log("적 턴");
            yield return new WaitForSeconds(1f);
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerDefense()
    {
        yield return new WaitForSeconds(1f);

        GameManager.instance.shield = 5;

        Debug.Log("적 턴");
        yield return new WaitForSeconds(1f);
        state = State.enemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerCounter()
    {
        yield return new WaitForSeconds(1f);

        GameManager.instance.counter = 3;
        GameManager.instance.counterAttack = true;

        Debug.Log("적 턴");
        yield return new WaitForSeconds(1f);
        state = State.enemyTurn;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        click = false;

        NewEnemy();
        Turn();

        if (GameManager.instance.hp <= 0)
        {
            state = State.loss;
            BatleEnd();
        }
        else
        {
            Debug.Log("플레이어 턴");
            GameManager.instance.counterAttack = false;
            state = State.playerTurn;
        }
    }

    void NewEnemy()
    {
        if (curEnemy == 0)
            return;

        enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], new Vector3(10, 3), Quaternion.identity);
        enemySlot[number] = enemy.GetComponent<Enemy>();
        number++;
        curEnemy--;
    }

    void Turn()
    {
        if (enemySlot[0].transform.position == attackLine) EnemyAttack();
        else
        {
            for (int i = 0; i < enemySlot.Length; i++)
            {
                if (enemySlot[i] == null)
                    break;

                Vector3 trans = enemySlot[i].transform.position;
                enemySlot[i].transform.position = new Vector3(trans.x - 2, trans.y);
            }
        }
    }

    void EnemyAttack()
    {
        if (GameManager.instance.counterAttack == true) Counter();
        else if(GameManager.instance.shield > 0)
        {
            GameManager.instance.shield -= enemySlot[0].damage;
            if (GameManager.instance.shield < 0) GameManager.instance.hp += GameManager.instance.shield;
        }
        else GameManager.instance.hp -= enemySlot[0].damage;
    }

    void Counter()
    {
        if(GameManager.instance.shield > 0)
        {
            if (enemySlot[0].damage > GameManager.instance.counter)
            {
                GameManager.instance.shield -= enemySlot[0].damage * 2;
                if (GameManager.instance.shield < 0) GameManager.instance.hp += GameManager.instance.shield;
            }
            else if (enemySlot[0].damage == GameManager.instance.counter) enemySlot[0].hp -= enemySlot[0].damage * 2;
            else
            {
                GameManager.instance.shield -= enemySlot[0].damage / 2;
                if (GameManager.instance.shield < 0)
                    GameManager.instance.hp += GameManager.instance.shield;
                enemySlot[0].hp -= enemySlot[0].damage;
            }
        }
        else
        {
            if (enemySlot[0].damage > GameManager.instance.counter)
            {
                GameManager.instance.hp -= enemySlot[0].damage * 2;
            }
            else if (enemySlot[0].damage == GameManager.instance.counter) enemySlot[0].hp -= enemySlot[0].damage * 2;
            else
            {
                GameManager.instance.hp -= enemySlot[0].damage / 2;
                enemySlot[0].hp -= enemySlot[0].damage;
            }
        }
    }

    void BatleEnd()
    {
        if(GameManager.instance.hp > 0)   Debug.Log("게임 승리");
        if (GameManager.instance.hp <= 0) Debug.Log("게임 패배");
    }
}

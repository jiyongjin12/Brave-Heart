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
    public Transform playerBattleTrans; //�÷��̾��� ��ǥ
    public Enemy[] enemySlot;

    public int enemyCount; //0�� �Ǹ� �÷��̾� �¸�
    public int curEnemy; //������ ���ʹ� ��

    public GameObject enemy;

    public int number;
    private Vector3 attackLine = new Vector3(-4, 3);

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
        if (state != State.playerTurn)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);


        if (enemySlot[0].hp > 0)
            enemySlot[0].hp -= GameManager.instance.playerDamage;

        if (enemyCount == 0)
        {
            state = State.win;
            BatleWin();
        }
        else
        {
            Debug.Log("�� ��");
            yield return new WaitForSeconds(1f);
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        NewEnemy();
        Turn();

        if (GameManager.instance.hp == 0)
        {
            state = State.loss;
            BatleWin();
        }
        else
        {
            Debug.Log("�÷��̾� ��");
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
        if (enemySlot[0].transform.position == attackLine)
            GameManager.instance.hp -= enemySlot[0].damage;
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

    void BatleWin()
    {
        Debug.Log("���� �¸�");
    }
}

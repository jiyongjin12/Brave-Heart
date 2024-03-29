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
        for (int i = 0; i < 3; i++)
        {
            enemy = Instantiate(enemyPrefab[Random.Range(0, 3)], new Vector3(4 + i * 2, 3), Quaternion.identity);
            enemySlot[i] = enemy.GetComponent<Enemy>();
            GameManager.instance.number++;
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

        //enemySlot[1].hp -= GameManager.instance.playerDamage;

        if (enemyCount == 0)
        {
            state = State.win;
            BatleWin();
        }
        else
        {
            Debug.Log("�� ��");
            state = State.enemyTurn;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        //enemys.Turn();

        if (GameManager.instance.hp == 0)
        {
            state = State.loss;
            BatleWin();
        }
        else
        {
            Debug.Log("�� ��");
            state = State.playerTurn;
        }
    }

    void BatleWin()
    {
        Debug.Log("���� �¸�");
    }
}

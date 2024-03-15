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
    public Transform[] enemyBattleTrans; //���ʹ� ��ǥ

    public int enemyCount; //0�� �Ǹ� �÷��̾� �¸�
    public int curEnemy; //������ ���ʹ� ��

    public float Num;
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
            Instantiate(enemyPrefab[Random.Range(0, 3)], enemyBattleTrans[i]);
            Num++;
        }
        state = State.playerTurn;
    }

    public void PlayerAttackButton()
    {
        if (state != State.playerTurn)
            return;

        Num = 0;
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        yield return new WaitForSeconds(1f);

        if(Enemy.instance.enemyNum == 0)
            Enemy.instance.hp -= GameManager.instance.playerDamage;
        else
            Enemy.instance.hp -= 0;

        if (enemyCount == 0)
        {
            state = State.win;
            BatleWin();
        }
        else
        {
            Debug.Log("���� ����");
            //state = State.enemyTurn;
        }
    }

    void BatleWin()
    {
        Debug.Log("���� �¸�");
    }
}

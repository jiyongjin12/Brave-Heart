using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float playerDamage;
    public float[] enmeyDamage;

    public float hp;
    public float Maxhp;

    public float[] enemyHp;
    public float[] maxEnemyHP;

    public static GameManager instance { get; private set; }
    public enum State
    {
        start, playerTurn, enemyTurn, win, loss
    }

    public State state;
    public bool isLive;

    private void Awake()
    {
        instance = this;
        isLive = true;
        hp = Maxhp;
        state = State.start;
        BattleStart();
    }

    private void BattleStart()
    {
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


        if (!isLive)
        {
            state = State.win;
            BatleWin();
        }
        else
        {
            state = State.enemyTurn;
        }
    }

    void BatleWin()
    {
        Debug.Log("°ÔÀÓ ½Â¸®");
    }
}

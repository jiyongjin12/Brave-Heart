using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject[] enemyPrefab;
    [SerializeField]
    private GameObject[] button;
    [SerializeField]
    private GameObject shieldIcon;

    public static BattleSystem instance { get; private set; }
    public Transform playerBattleTrans; //플레이어의 좌표
    public Enemy[] enemySlot;

    public int enemyCount; //0이 되면 플레이어 승리
    public int curEnemy; //생성될 에너미 수

    public GameObject enemy;

    public int number = 0;
    private Vector3 attackLine = new Vector3(-4, 3);
    private Vector3 ArcherAttackLine = new Vector3(-5, 3);

    private float battleMotion = 0;
    private bool click = false;

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
        shieldIcon.SetActive(false);
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

        battleMotion = 1;
        click = true;
            
        StartCoroutine(PlayerTurn());
    }

    public void PlayerDefenseButton()
    {
        if (state != State.playerTurn || click)
            return;

        battleMotion = 2;
        click = true;
        StartCoroutine(PlayerTurn());
    }

    public void PlayerCounterButton()
    {
        if (state != State.playerTurn || click)
            return;

        battleMotion = 3;
        click = true;
        StartCoroutine(PlayerTurn());
    }

    IEnumerator PlayerTurn()
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(false);
        }
        yield return new WaitForSeconds(1f);

        if (battleMotion == 1)
        {
            if (enemySlot[0].hp > 0)
            {
                Debug.Log("공격");
                enemySlot[0].hp -= GameManager.instance.playerDamage;
            }
        }    
        else if (battleMotion == 2)
        {
            Debug.Log("쉴드 생성");
            shieldIcon.SetActive(true);
            GameManager.instance.shield = 5;
        }
        else
        {
            Debug.Log("카운터");
            GameManager.instance.counter = 3;
            GameManager.instance.counterAttack = true;
        }

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

    public IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        NewEnemy();
        Turn();

        if (GameManager.instance.hp <= 0)
        {
            state = State.loss;
            BatleEnd();
        }
        else if(enemyCount == 0)
        {
            state = State.win;
            BatleEnd();
        }
        else
        {
            Debug.Log("플레이어 턴");
            GameManager.instance.counterAttack = false;
            click = false;
            for (int i = 0; i < button.Length; i++)
            {
                button[i].SetActive(true);
            }
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
        if (enemyCount == 0)
            return;

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
            ShieldBreak();
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
                ShieldBreak();
            }
            else if (enemySlot[0].damage == GameManager.instance.counter)
            {
                enemySlot[0].hp -= enemySlot[0].damage * 2;
            }
            else
            {
                GameManager.instance.shield -= enemySlot[0].damage / 2;
                ShieldBreak();
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

    void ShieldBreak()
    {
        if (GameManager.instance.shield < 0)
        {
            shieldIcon.SetActive(false);
            GameManager.instance.hp += GameManager.instance.shield;
        }
    }

    void BatleEnd()
    {
        if (GameManager.instance.hp > 0)  Debug.Log("게임 승리");
        if (GameManager.instance.hp <= 0) Debug.Log("게임 패배");
    }
}

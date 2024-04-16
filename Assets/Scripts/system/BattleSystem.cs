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
    private Vector3 newEnemyPos = new Vector3(10, 3);

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

    private void FixedUpdate()
    {
        if (enemyCount == 0)
        {
            state = State.win;
            BatleEnd();
        }
        else if (GameManager.instance.hp <= 0)
        {
            state = State.loss;
            BatleEnd();
        }
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
            Debug.Log("공격");
            enemySlot[0].hp -= GameManager.instance.playerDamage;
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

        Debug.Log("적 턴");
        yield return new WaitForSeconds(2f);
        state = State.enemyTurn;
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        NewEnemy();
        StartCoroutine(Turn());
    }

    void NewEnemy()
    {
        if (curEnemy == 0 || enemySlot[6] != null)
            return;

        enemy = Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Length)], newEnemyPos, Quaternion.identity);
        enemySlot[number] = enemy.GetComponent<Enemy>();
        number++;
        curEnemy--;
    }

    IEnumerator Turn()
    {
        if (enemySlot[0].transform.position == attackLine && Enemy.instance.enemyType != Enemy.EnemyType.Archer ||
            Enemy.instance.enemyType != Enemy.EnemyType.Wizard && enemySlot[0].transform.position == attackLine)
        {
            EnemyAttack(0);
            StartCoroutine(Attack());
        }
        else
        {
            for (int i = 0; i < enemySlot.Length; i++)
            {
                if (enemySlot[i] == null || enemySlot[i].isMovement)
                    break;

                Vector3 trans = enemySlot[i].transform.position;
                enemySlot[i].transform.position = new Vector3(trans.x - 2, trans.y);
                yield return new WaitForSeconds(0.5f);
                if (enemySlot[i].enemyType == Enemy.EnemyType.Archer)
                    Enemy.instance.AttackArcher(i);
                if (enemySlot[i].enemyType == Enemy.EnemyType.Wizard)
                    Enemy.instance.AttackWizard(i);
                yield return new WaitForSeconds(1f);
                if (enemySlot[i] == null) i--;
            }
        }

        Debug.Log("플레이어 턴");
        GameManager.instance.counterAttack = false;
        click = false;
        for (int i = 0; i < button.Length; i++)
        {
            button[i].SetActive(true);
        }
        int num = 0;
        for (int j = 0; j < 10; j++)
        {
            if (enemySlot[j] == null && enemySlot[j + 1] == null)
            {
                num++;
            }
            else if (enemySlot[j] == null && enemySlot[j + 1] != null)
            {
                enemySlot[j - num] = enemySlot[j + 1];
                enemySlot[j + 1] = null;
            }
        }
        state = State.playerTurn;
    }

    public IEnumerator Attack()
    {
        for(int i = 0; i < enemySlot.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (enemySlot[i].enemyType == Enemy.EnemyType.Archer)
                Enemy.instance.AttackArcher(i);
            else if (enemySlot[i].enemyType == Enemy.EnemyType.Wizard)
                Enemy.instance.AttackWizard(i);
            else
                break;
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void EnemyAttack(int num)
    {

        if (GameManager.instance.counterAttack == true) Counter(num);
        else if(GameManager.instance.shield > 0)
        {
            GameManager.instance.shield -= enemySlot[num].damage;
            ShieldBreak();
        }
        else GameManager.instance.hp -= enemySlot[num].damage;
    }

    void Counter(int num)
    {
        if(GameManager.instance.shield > 0)
        {
            if (enemySlot[num].damage > GameManager.instance.counter)
            {
                GameManager.instance.shield -= enemySlot[num].damage * 2;
                ShieldBreak();
            }
            else if (enemySlot[num].damage == GameManager.instance.counter)
            {
                enemySlot[num].hp -= enemySlot[num].damage * 2;
            }
            else
            {
                GameManager.instance.shield -= enemySlot[num].damage / 2;
                ShieldBreak();
                enemySlot[num].hp -= enemySlot[num].damage;
            }
        }
        else
        {
            if (enemySlot[num].damage > GameManager.instance.counter)
            {
                GameManager.instance.hp -= enemySlot[num].damage * 2;
            }
            else if (enemySlot[num].damage == GameManager.instance.counter) enemySlot[num].hp -= enemySlot[num].damage * 2;
            else
            {
                GameManager.instance.hp -= enemySlot[num].damage / 2;
                enemySlot[num].hp -= enemySlot[num].damage;
            }
        }
    }

    void ShieldBreak()
    {
        if (GameManager.instance.shield <= 0)
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

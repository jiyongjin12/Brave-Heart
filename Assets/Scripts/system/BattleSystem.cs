using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

[System.Serializable]
public class EnemyPer
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    [Range(0, 100)]
    private float chance = 0;

    public float Weight { set; get; }

    public GameObject Prefab => prefab;
    public float Chance => chance;
}

public class BattleSystem : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public EnemyPer[] enemyPrefab;
    [SerializeField]
    private GameObject[] button;

    public GameObject shieldIcon;

    public static BattleSystem instance { get; private set; }
    public Transform playerBattleTrans; //플레이어의 좌표
    public Enemy[] enemySlot;

    public int enemyCount; //0이 되면 플레이어 승리
    public int curEnemy; //생성될 에너미 수

    public GameObject enemy;

    public int number = 0;
    private Vector3 newEnemyPos = new Vector3(12, 5);

    public float battleMotion = 0;
    private float acc;

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
        EnemyPercent();
    }

    private void Start()
    {
        BattleStart();
    }

    private void FixedUpdate()
    {
        battle();
    }

    void battle()
    {
        if (enemyCount <= 0)
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

    private void EnemyPercent()
    {
        acc = 0;
        foreach(var item in enemyPrefab)
        {
            acc += item.Chance;
            item.Weight = acc;
        }
    }

    private void SpawnEnemy(Vector2 pos)
    {
        var clone = enemyPrefab[GetRandom()];
        
        enemy = Instantiate(clone.Prefab, pos, Quaternion.identity);
        Enemy.instance.EnemyPos(clone.Prefab.GetComponent<Enemy>());
        enemy.transform.position = new Vector3(pos.x, Enemy.instance.yPos);
        enemySlot[number] = enemy.GetComponent<Enemy>();
        GameManager.instance.SpawnEnemyHPSlider(enemySlot[number]);
    }

    private int GetRandom()
    {
        float random = Random.value * acc;

        for(int i = 0; i < enemyPrefab.Length; ++i)
        {
            if(enemyPrefab[i].Weight >= random)
            {
                return i;
            }
        }

        return 0;
    }

    private void BattleStart()
    {
        for (int i = 0; i < 4; i++)
        {
            SpawnEnemy(new Vector2(4 + i * 2, 5));
            number++;
            curEnemy--;
        }
        state = State.playerTurn;
    }

    public IEnumerator PlayerTurn()
    {
        yield return YieldCache.WaitForSeconds(1f);

        if (battleMotion == 1)
        {
            Debug.Log("공격");
            Unit.instance.AttackMotion();
            yield return new WaitForSeconds(5f);
            //enemySlot[0].hp -= GameManager.instance.playerDamage;
        }    
        else if (battleMotion == 2)
        {
            Debug.Log("쉴드 생성");
            shieldIcon.SetActive(true);
            GameManager.instance.shield = GameManager.instance.playerDamage;
        }
        else
        {
            Debug.Log("카운터");
            GameManager.instance.counter = GameManager.instance.playerDamage;
            GameManager.instance.counterAttack = true;
        }

        yield return YieldCache.WaitForSeconds(1f);
        Debug.Log("적 턴");
        state = State.enemyTurn;
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        NewEnemy();
        yield return YieldCache.WaitForSeconds(0.5f);
        StartCoroutine(Turn());
    }

    void NewEnemy()
    {
        if (curEnemy <= 0 || enemySlot[8] != null)
            return;

        SpawnEnemy(newEnemyPos);
        number++;
        curEnemy--;
    }

    IEnumerator Turn()
    {
        if (enemySlot[0].transform.position.x == -6 && Enemy.instance.enemyType != Enemy.EnemyType.Archer ||
            Enemy.instance.enemyType != Enemy.EnemyType.Wizard && enemySlot[0].transform.position.x == -6)
        {
            GameManager.instance.EnemyAttack(0);
            StartCoroutine(GameManager.instance.Attack());
        }
        else
        {
            for (int i = 0; i < enemySlot.Length; i++)
            {
                if (enemySlot[i] == null)
                    continue;

                Vector3 trans = enemySlot[i].transform.position;
                StartCoroutine(MoveTo(enemySlot[i], new Vector3(trans.x - 2, trans.y)));
                yield return YieldCache.WaitForSeconds(0.5f);
                if (enemySlot[i].enemyType == Enemy.EnemyType.Archer)
                    Enemy.instance.AttackArcher(i);
                else if (enemySlot[i].enemyType == Enemy.EnemyType.Wizard)
                    Enemy.instance.AttackWizard(i);
                else if(enemySlot[i].enemyType == Enemy.EnemyType.Warrior && enemySlot[i].transform.position.x == -6)
                    GameManager.instance.EnemyAttack(0);
                yield return YieldCache.WaitForSeconds(1f);
                if (enemySlot[i] == null) i--;
            }
        }

        Debug.Log("플레이어 턴");
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
        GameManager.instance.counterAttack = false;
        GameManager.instance.isClick = false;
        DiceManager.instance.IsMyTurn();
        state = State.playerTurn;
    }

    //적 움직임
    IEnumerator MoveTo(Enemy a, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;

        while (true)
        {
            count += Time.deltaTime * 3;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }

    void BatleEnd()
    {
        Time.timeScale = 0;
        if (GameManager.instance.hp > 0)  Debug.Log("게임 승리");
        if (GameManager.instance.hp <= 0) Debug.Log("게임 패배");
    }
}

//코루틴 최적화
static class YieldCache
{
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds>
_timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        WaitForSeconds wfs;
        if (!_timeInterval.TryGetValue(seconds, out wfs))
            _timeInterval.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float playerDamage;

    public float hp;
    public float Maxhp;

    [SerializeField] private Image _hpBar;
    [SerializeField] private Text TextDefense;

    [SerializeField] private GameObject enemyHPSliderPrefab;
    [SerializeField] private GameObject enemyDamagePrefab;
    [SerializeField] private Transform canvasTransform;

    public GameObject Damage;

    public GameObject adc;

    public float shield;
    public float counter; //카운터 확률

    public bool counterAttack = false;

    private int emptyEnemy;
    public bool isClick;

    public bool isPause;
    public GameObject ClickObj;
    public AudioClip audio;
    //public GameObject ClickArea;

    private void Awake()
    {
        emptyEnemy = 1;
        instance = this;
        hp = GameSuvManager.instance.gameData.playerHP;
        Maxhp = GameSuvManager.instance.gameData.playerMaxHP;
        playerDamage = GameSuvManager.instance.gameData.playerDmg;
        counter = GameSuvManager.instance.gameData.playerCounter;
        isPause = false;
        counterAttack = false;
        isClick = false;
    }

    private void Update()
    {
        _hpBar.fillAmount =  hp / Maxhp;
        GameSuvManager.instance.gameData.playerMaxHP = Maxhp;
        TextDefense.text = shield.ToString();
        //if (BattleSystem.instance.state == BattleSystem.State.playerTurn)
        //{
        //    ClickArea.transform.position = BattleSystem.instance.enemySlot[BattleSystem.instance.TNum].transform.position;
        //    ClickArea.SetActive(true);
        //}
        //else
        //{
        //    ClickArea.SetActive(false);
        //}
        if (!isPause && Input.GetMouseButtonDown(0) && BattleSystem.instance.state == BattleSystem.State.playerTurn)
        {
            ClickObj = mouseGetObject();
            if (ClickObj != null && ClickObj.layer == LayerMask.NameToLayer("Monster"))
            {
                BattleSystem.instance.TNum = ClickObj.GetComponent<Enemy>().enemyNum;
                BattleSystem.instance.EliteTrans = new Vector3(BattleSystem.instance.enemySlot[BattleSystem.instance.TNum].transform.position.x, 5);
            }
        }
        if (isClick == true && adc.activeInHierarchy == true)
            adc.SetActive(false);
    }

    public void ADCTrue()
    {
        adc.SetActive(true);
    }

    public void SpawnEnemyHPSlider(Enemy enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localPosition = Vector3.one;

        sliderClone.GetComponent<HpPos>().SetUp(enemy.transform, enemy);
    }

    public void SpawnDamageText(Enemy enemy)
    {
        GameObject TextClone = Instantiate(enemyDamagePrefab);

        TextClone.transform.SetParent(canvasTransform);
        TextClone.transform.localPosition = Vector3.one;

        TextClone.GetComponent<Damage>().SetUp(enemy.transform);
    }

    public void TextDamage(Transform unit)
    {
        GameObject TextClone = Instantiate(Damage);

        TextClone.transform.SetParent(canvasTransform);
        TextClone.transform.localPosition = Vector3.one;

        TextClone.GetComponent<Damage>().SetUp(unit.transform);
    }

    public void playerTurn()
    {
        if (BattleSystem.instance.battleMotion == 0 || isClick != false || DiceManager.instance.diceRollingList.Count != 0)
            return;

        isClick = true;
        StartCoroutine(BattleSystem.instance.PlayerTurn());
    }
    //적이 죽었을 때 배열 변경
    public void DeadEnmey()
    {
        emptyEnemy = 0;
        for (int i = 0; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            if(i == BattleSystem.instance.enemySlot.Length - 1) break;
            else if (BattleSystem.instance.enemySlot[i] == null && BattleSystem.instance.enemySlot[i + 1] != null)
            {
                Debug.Log("ON");
                BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i + 1];
                BattleSystem.instance.enemySlot[i + 1] = null;
                continue;
            }
            else if(BattleSystem.instance.enemySlot[i + 1] == null && BattleSystem.instance.enemySlot[i] == null)
            {
                Debug.Log("OFF");
                emptyEnemy++;
                continue;
            }
        }
        
    }

    public void DeadEnmeies()
    {
        emptyEnemy = 1;
        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            if (i == BattleSystem.instance.enemySlot.Length - 1) break;
            else if (BattleSystem.instance.enemySlot[i] == null && BattleSystem.instance.enemySlot[i + 1] != null)
            {
                Debug.Log("ON");
                BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i + 1];
                BattleSystem.instance.enemySlot[i + 1] = null;
                continue;
            }
            else if (BattleSystem.instance.enemySlot[i + 1] == null && BattleSystem.instance.enemySlot[i] == null)
            {
                Debug.Log("OFF");
                emptyEnemy++;
                continue;
            }
        }

    }

    public void EnemyAttack(int num)
    {
        SoundManager.instance.SFXPlay("Attack", audio);
        StartCoroutine(AttackRot(num));
        if (counterAttack == true) Counter(num);
        else if (shield > 0)
        {
            shield -= BattleSystem.instance.enemySlot[num].damage;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage);
            TextDamage(BattleSystem.instance.playerBattleTrans);
            ShieldBreak();
        }
        else
        {
            hp -= BattleSystem.instance.enemySlot[num].damage;
            StartCoroutine(Unit.instance.ShakePlayer());
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage);
            TextDamage(BattleSystem.instance.playerBattleTrans);
        }
    }

    IEnumerator AttackRot(int num)
    {
        BattleSystem.instance.enemySlot[num].transform.rotation = Quaternion.Euler(0, 0, 10);
        yield return YieldCache.WaitForSeconds(0.5f);
        BattleSystem.instance.enemySlot[num].transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public int GetRandom()
    {
        float random = Random.value * 100;

        if (counter >= random)
        {
            return 1;
        }

        return 0;
    }

    public void Counter(int num)
    {
        float Success = GetRandom();
        if (Success == 0)
        {
            if(shield > 0)
            {
                shield -= BattleSystem.instance.enemySlot[num].damage * 2;
                ShieldBreak();
            }
            else
            {
                hp -= BattleSystem.instance.enemySlot[num].damage * 2;
                StartCoroutine(Unit.instance.ShakePlayer());
            }
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage * 2);
            TextDamage(BattleSystem.instance.playerBattleTrans);
        }
        else
        {
            StartCoroutine(Enemy.instance.ShakeMonster(num));
            BattleSystem.instance.enemySlot[num].hp -= playerDamage * 1.5f;
            Unit.instance.TextEnemyDamage(playerDamage * 1.5f);
            SpawnDamageText(BattleSystem.instance.enemySlot[num]);
        }
    }

    public void ShieldBreak()
    {
        if (shield <= 0)
        {
            BattleSystem.instance.shieldIcon.SetActive(false);
            StartCoroutine(Unit.instance.ShakePlayer());
            hp += shield;
        }
    }

    public GameObject mouseGetObject()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        GameObject clickObject = null;
        if (hit.collider != null)
        {
            clickObject = hit.transform.gameObject;
            return clickObject;
        }
        return null;
    }
}

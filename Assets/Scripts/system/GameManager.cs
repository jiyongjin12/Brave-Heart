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
    public float counter;

    public bool counterAttack = false;

    private int emptyEnemy;
    public bool isClick;

    private void Awake()
    {
        emptyEnemy = 1;
        instance = this;
        hp = Maxhp;
        counterAttack = false;
        isClick = false;
    }

    private void Update()
    {
        _hpBar.fillAmount =  hp / Maxhp;
        TextDefense.text = shield.ToString();
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

        sliderClone.GetComponent<HpPos>().SetUp(enemy.transform);
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
        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            
            if (BattleSystem.instance.enemySlot[i - emptyEnemy] == null)
            {
                BattleSystem.instance.number--;
                emptyEnemy++;
                break;
            }
            else BattleSystem.instance.enemySlot[i - emptyEnemy] = BattleSystem.instance.enemySlot[i];

            if (i == BattleSystem.instance.enemySlot.Length - emptyEnemy)
            {
                BattleSystem.instance.enemySlot[i] = null;
            }
        }
        emptyEnemy = 1;
    }

    public IEnumerator Attack()
    {
        for (int i = 1; i < BattleSystem.instance.enemySlot.Length; i++)
        {
            yield return YieldCache.WaitForSeconds(0.5f);
            if (BattleSystem.instance.enemySlot[i].enemyType == Enemy.EnemyType.Archer)
                Enemy.instance.AttackArcher(i);
            else if (BattleSystem.instance.enemySlot[i].enemyType == Enemy.EnemyType.Wizard)
                Enemy.instance.AttackWizard(i);
            else
                break;
            yield return YieldCache.WaitForSeconds(1f);
        }

    }

    public void EnemyAttack(int num)
    {

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

    public void Counter(int num)
    {
        if (BattleSystem.instance.enemySlot[num].damage > counter && shield > 0)
        {
            shield -= BattleSystem.instance.enemySlot[num].damage * 2;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage * 2);
            TextDamage(BattleSystem.instance.playerBattleTrans);
            ShieldBreak();
        }
        else if (BattleSystem.instance.enemySlot[num].damage > counter)
        {
            hp -= BattleSystem.instance.enemySlot[num].damage * 2;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage * 2);
            StartCoroutine(Unit.instance.ShakePlayer());
            TextDamage(BattleSystem.instance.playerBattleTrans);
        }
        else if (BattleSystem.instance.enemySlot[num].damage == counter)
        {
            StartCoroutine(Enemy.instance.ShakeMonster(num));
            BattleSystem.instance.enemySlot[num].hp -= BattleSystem.instance.enemySlot[num].damage * 2;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage * 2);
            SpawnDamageText(BattleSystem.instance.enemySlot[num]);
        }
        else
        {
            StartCoroutine(Enemy.instance.ShakeMonster(num));
            BattleSystem.instance.enemySlot[num].hp -= BattleSystem.instance.enemySlot[num].damage;
            Unit.instance.TextEnemyDamage(BattleSystem.instance.enemySlot[num].damage);
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
}

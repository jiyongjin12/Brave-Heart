using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public static Unit instance { get; private set; }
    Vector3 playerPos = new Vector3(-8, 4);

    private Animator animator;

    public bool isAttacking;

    public GameObject Damage;
    public float CurDamage;

    private void Awake()
    {
        instance = this;
        isAttacking = false;
        animator = GetComponent<Animator>();
    }

    public void AttackMotion()
    {
        StartCoroutine(playerAttack());
    }

    IEnumerator playerAttack()
    {
        isAttacking = true;
        StartCoroutine(PlayerMoveTo(new Vector3(BattleSystem.instance.enemySlot[0].transform.position.x - 2, 4)));
        animator.SetInteger("Attack", 1);
        yield return YieldCache.WaitForSeconds(5f);
        transform.position = playerPos;
    }

    //스킬 베기 데미지
    public void SleshDamage()
    {
        BattleSystem.instance.enemySlot[0].hp -= GameManager.instance.playerDamage / 6;
        GameManager.instance.SpawnDamageText(BattleSystem.instance.enemySlot[0]);
        CurDamage = (GameManager.instance.playerDamage / 6);

    }
    //스킬 찍기 데미지
    public void EndDamage()
    {
        BattleSystem.instance.enemySlot[0].hp -= GameManager.instance.playerDamage / 2;
        GameManager.instance.SpawnDamageText(BattleSystem.instance.enemySlot[0]);
        CurDamage = (GameManager.instance.playerDamage / 2);
        animator.SetInteger("Attack", 0);
        isAttacking = false;
    }

    public void TextEnemyDamage(float damages)
    {
        CurDamage = damages;
    }

    //motion
    public void SleshMotion()
    {
        StopCoroutine(Enemy.instance.ShakeMonster(0));
        StartCoroutine(Enemy.instance.ShakeMonster(0));
    }

    public void EndMotion()
    {
        StopCoroutine(Enemy.instance.ShakeMonster(0));
        StartCoroutine(Enemy.instance.ShakeMonster(0));
    }

    public IEnumerator ShakePlayer()
    {
        float shakeTime = 2f;
        float shakePower = 0.3f;
        Vector3 origin = playerPos;

        while (shakeTime >= 0f)
        {
            shakeTime -= 0.05f;
            transform.position = origin + (Vector3)Random.insideUnitCircle * shakePower * shakeTime;
            yield return null;
        }

        transform.position = origin;
    }

    public void DefenseMotion()
    {

    }

    public void CounterMotion()
    {

    }

    IEnumerator PlayerMoveTo(Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = transform.position;

        while (true)
        {
            count += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
}

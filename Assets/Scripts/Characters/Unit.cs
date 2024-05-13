using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static Unit instance { get; private set; }
    Vector3 playerPos = new Vector3(-8, 5);


    private void Awake()
    {
        instance = this;
    }

    public void AttackMotion()
    {
        StartCoroutine(playerAttack());
    }

    IEnumerator playerAttack()
    {
        StartCoroutine(PlayerMoveTo(new Vector3(BattleSystem.instance.enemySlot[0].transform.position.x - 2, 5)));
        yield return YieldCache.WaitForSeconds(1f);
        transform.position = playerPos;
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

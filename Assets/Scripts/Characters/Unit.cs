using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public float damage;

    public float maxHp;
    public float curHp;

    public static float enmeyNum;

    public bool TakeDamage(float dmg)
    {
        curHp -= dmg;

        if (curHp <= 0)
            return true;
        else
            return false;
    }

    public bool EnemyHit(float dmg)
    {
        if(enmeyNum == 1)
        {
            curHp -= dmg;
        }

        if (curHp <= 0)
            return true;
        else
            return false;
    }
}

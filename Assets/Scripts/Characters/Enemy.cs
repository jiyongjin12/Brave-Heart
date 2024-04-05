using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public int Level;

    public float hp;
    public float damage;
    [SerializeField]
    private GameObject ray;

    private bool deadEnemy = false;

    //public static Enemy instance { get; private set; }

    private void Awake()
    {
        //instance = this;
        hp = enemyData.baseHp + enemyData.maxHp[Level];
        damage = enemyData.baseDamage + enemyData.damages[Level];
    }

    private void Update()
    {
        if(hp <= 0) StartCoroutine(Dead());
        if (deadEnemy == true)
            deadEnemy = false;
    }

    IEnumerator Dead()
    {
        deadEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        GameManager.instance.DeadEnmey();
        BattleSystem.instance.enemyCount--;
    }

    public void LevelUp()
    {
        Level++;
    }
}

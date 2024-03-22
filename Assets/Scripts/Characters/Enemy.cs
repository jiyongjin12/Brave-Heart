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


    RaycastHit2D hit;

    public static Enemy instance { get; private set; }

    private void Awake()
    {
        instance = this;
        hp = enemyData.baseHp + enemyData.maxHp[Level];
        damage = enemyData.baseDamage + enemyData.damages[Level];
    }

    private void Update()
    {
        if(hp <= 0)
        {
            StartCoroutine(Dead());
        }
        if(deadEnemy == true)
            deadEnemy = false;

        hit = Physics2D.Raycast(ray.transform.position , Vector2.left);
        Debug.DrawRay(ray.transform.position, Vector2.left * hit.distance, Color.red);
    }

    IEnumerator Dead()
    {
        deadEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        BattleSystem.instance.enemyCount--;
    }

    
    public void Turn()
    {
        if (hit.collider.tag == "Player")
            GameManager.instance.hp -= GameManager.instance.enemySlot[0].damage;
        else if (hit.collider.tag == "Enemy")
            return;
        else
        {
            for (int i = 0; i < GameManager.instance.enemySlot.Count; i++)
            {
                Vector2 trans = GameManager.instance.enemySlot[i].transform.position;
                trans = new Vector3(trans.x - 2, trans.y);
            }
        }
            
    }

    public void LevelUp()
    {
        Level++;
    }
}

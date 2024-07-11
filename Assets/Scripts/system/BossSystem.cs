using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSystem : MonoBehaviour
{
    public GameObject Boss;

    public EnemyPer[] enemyPrefab;
    public Enemy[] enemySlot;

    public int TNum = 0;
    public int number;

    private float acc;

    public int minusNum;

    public bool deadEnemy = false;
    public int num;
    private Vector3 newEnemyPos = new Vector3(8, 4f);

    private void Awake()
    {
        Instantiate(Boss, new Vector3(10, 4), Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

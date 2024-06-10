using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpfill : MonoBehaviour
{
    public Enemy enemy;
    private Slider _hp;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = BattleSystem.instance.enemySlot[BattleSystem.instance.number];
        _hp = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        this._hp.value = this.enemy.hp / this.enemy.Maxhp;
    }
}

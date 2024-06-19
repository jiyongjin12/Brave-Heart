using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hpfill : MonoBehaviour
{
    public Enemy enemy;
    private Slider _hp;
    public Text hpText;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = BattleSystem.instance.enemySlot[BattleSystem.instance.number];
        _hp = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        HP();
        
    }

    private void HP()
    {
        float curHP;
        curHP = float.Parse(enemy.hp.ToString("N2"));
        _hp.value = curHP / enemy.Maxhp;
        if (enemy.hp <= 0) hpText.text = "0/" + enemy.Maxhp;
        else hpText.text = curHP + "/" + enemy.Maxhp;
    }
}

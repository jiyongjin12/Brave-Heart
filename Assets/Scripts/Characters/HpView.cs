using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    //[SerializeField] private Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        _hpBar.fillAmount = BattleSystem.instance.enemySlot[BattleSystem.instance.number].hp / BattleSystem.instance.enemySlot[BattleSystem.instance.number].Maxhp;
        //hpText.text = enemy.hp + "/" + enemy.Maxhp;
    }
}

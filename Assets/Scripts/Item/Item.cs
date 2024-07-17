using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class Item : ItemData
{

    private void Awake()
    {
        this.itemIcon = itemIcon;
        this.itemNum = itemNum;
        this.itemDesc = itemDesc;
    }

    public void PessiveItem()
    {
        switch (itemName)
        {
            case ItemName.HPup:
                GameSuvManager.instance.gameData.playerMaxHP += Hp;
                break;
            case ItemName.DamageUp:
                GameSuvManager.instance.gameData.playerDmg += Damage;
                break;
            case ItemName.CounterUp:
                GameSuvManager.instance.gameData.playerCounter += Counter;
                break;
        }
    }

    public void ActiveItem()
    {
        switch (itemName)
        {
            case ItemName.HPup:
                GameManager.instance.hp += Hp;
                break;
            case ItemName.DamageUp:
                GameManager.instance.playerDamage += Damage;
                break;
            case ItemName.CounterUp:
                GameManager.instance.counter += Counter;
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class Item : ScriptableObject
{
    public enum ItemType { SingleAttack, AttackAll, HpUp, DamageUp,  ShieldUp}
    public ItemType itemType;
    public string ItemName;

    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    public float baseDamage;
    public float baseHp;
    public float[] damages;
    public float[] hps;
}

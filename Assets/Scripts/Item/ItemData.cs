using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public enum ItemType { Pessive, Active }
    public enum ItemName { HPup, DamageUp, CounterUp }

    public ItemType itemType;
    public ItemName itemName;

    public Sprite itemIcon;

    public string itemNum; //보일 아이템 이름
    public string itemDesc; //아이템 설명

    public float Damage; //데미지 관련된 아이템일때 사용
    public float Hp; //체력 관련 아이템일때 사용
    public float Counter; //카운터 확률관련 아이템일때 사용
}

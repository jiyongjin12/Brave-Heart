using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item", menuName = "ShopItem/Shop Item", order = 4)]
public class Shop_so : ScriptableObject
{
    [SerializeField] public List<ItemSO> shopItem;
    public List<ItemSO> ShopItem => shopItem;
}

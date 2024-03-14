using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType { Slime, Goblin, Orc, Humman}
    public EnemyType enemyType;
    public string EnemyName;
    [TextArea]
    public string EnemyDesc;

    public int unitLevel;

    public float damage;
    public float[] damages;

    public float maxHp;
    public float curHp;
}

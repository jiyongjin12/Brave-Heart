using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType { Slime, Goblin, Orc, Humman}
    public EnemyType enemyType;
    public int EnemyId;
    public string EnemyName;
    [TextArea]
    public string EnemyDesc;

    public float baseDamage;
    public int basehp;
    public float[] damages;
    public int[] hp;
}

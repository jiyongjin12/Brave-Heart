using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType { Almost, Archer, Boss}
    public EnemyType enemyType;
    public string EnemyName;

    public float baseDamage;
    public float baseHp;

    public float[] damages;
    public float[] maxHp;
}

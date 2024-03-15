using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float playerDamage;

    public float hp;
    public float Maxhp;

    private void Awake()
    {
        instance = this;
        hp = Maxhp;
    }
}

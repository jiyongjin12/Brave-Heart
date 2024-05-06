using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static Unit instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void AttackMotion()
    {
        
    }

    public void DefenseMotion()
    {

    }

    public void CounterMotion()
    {

    }
}

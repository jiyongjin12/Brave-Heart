using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float hp;
    private void Awake()
    {
        GameManager.instance.Maxhp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

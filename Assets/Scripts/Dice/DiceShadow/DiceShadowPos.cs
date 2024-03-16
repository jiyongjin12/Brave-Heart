using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShadowPos : MonoBehaviour
{
    public GameObject Dice;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Dice.transform.position, 10000000);
    }
}

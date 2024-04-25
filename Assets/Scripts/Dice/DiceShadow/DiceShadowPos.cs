using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShadowPos : MonoBehaviour
{
    public GameObject Dice;
    public GameObject DiceShadow;

    private void Update()
    {
        if (Dice.GetComponent<Dice>().SelectedDice) DiceShadow.SetActive(false);
        else DiceShadow.SetActive(true);

        transform.position = Vector2.MoveTowards(transform.position, Dice.transform.position, 10000000);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoardClick : MonoBehaviour
{
    public void BoardClickEvent()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DiceManager.instance.isBool = true;
        }
    }
}

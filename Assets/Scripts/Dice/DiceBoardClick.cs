using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBoardClick : MonoBehaviour
{
    public void BoardClickEvent()
    {
        DiceManager.instance.isBool = true;
    }
}

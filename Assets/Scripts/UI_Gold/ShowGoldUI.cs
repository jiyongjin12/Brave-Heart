using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowGoldUI : MonoBehaviour
{
    [SerializeField] private TMP_Text GoldCoundText;

    private void Update()
    {
        GoldCoundText.text = GameSuvManager.instance.gameData.Gold.ToString();
    }
}

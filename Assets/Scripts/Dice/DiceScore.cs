using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceScore : MonoBehaviour
{
    public float DamageScore = 0;
    public bool fullHouse;
    public bool fourOfKind;
    public bool straight;
    public bool yacht;
    public TextEffect fullHouseText;
    public TextEffect fourOfKingText;
    public TextEffect straightText;
    public TextEffect yachtText;

    public TMP_Text DamageText;
    public TMP_Text ADCText;

    private void Update()
    {
        //DiceManager의 인스턴스를 가져옴
        DiceManager manager = DiceManager.instance;

        // DiceManager에 있는 selectedDice.finalSide 값을 가져와서 배열에 저장
        int sum = 0;
        foreach (var die in manager.selectedDice)
        {
            if (die != null)
            {
                sum += (int)die.finalSide;
            }
        }
        DamageScore = sum;

        GameManager.instance.playerDamage = DamageScore;
        DamageText.text = DamageScore.ToString("0.0");

        // DiceManager에 있는 diceList.finalSide 값을 가져와서 배열에 저장
        int[] sides = new int[manager.diceList.Count];
        for (int i = 0; i < manager.diceList.Count; i++)
        {
            sides[i] = manager.diceList[i].finalSide;
        }

        System.Array.Sort(sides);

        if (DiceManager.instance.StartSetUpBool == true)
        {
            CheckFullHouse(sides);
            CheckFourOfAKind(sides);
            CheckStraight(sides);
            CheckYacht(sides);
        }
            
    }

    public void PlayerAttackButton()
    {
        if (BattleSystem.instance.state != BattleSystem.State.playerTurn)
            return;

        BattleSystem.instance.battleMotion = 1;
        ADCText.text = "Attack";
        GameManager.instance.adc.SetActive(false);
    }

    public void PlayerDefenseButton()
    {
        if (BattleSystem.instance.state != BattleSystem.State.playerTurn)
            return;

        BattleSystem.instance.battleMotion = 2;
        ADCText.text = "Defense";
        GameManager.instance.adc.SetActive(false);
    }

    public void PlayerCounterButton()
    {
        if (BattleSystem.instance.state != BattleSystem.State.playerTurn)
            return;

        BattleSystem.instance.battleMotion = 3;
        ADCText.text = "Counter";
        GameManager.instance.adc.SetActive(false);
    }

    // Full House
    private void CheckFullHouse(int[] sides)
    {
        if ((sides[0] == sides[2] && sides[3] == sides[4] && sides[0] != sides[3]) ||
        (sides[0] == sides[1] && sides[2] == sides[4] && sides[0] != sides[2]))
        {
            fullHouse = true;
            fullHouseText.WobbleText();
        }
        else
        {
            fullHouse = false;
            fullHouseText.ResetTextWaveMent();
        }
    }

    // 4 of a Kind
    private void CheckFourOfAKind(int[] sides)
    {
        if ((sides[0] == sides[3] && sides[0] != sides[4]) ||
        (sides[1] == sides[4] && sides[0] != sides[1]))
        {
            fourOfKind = true;
            fourOfKingText.WobbleText();
        }
        else
        {
            fourOfKind = false;
            fourOfKingText.ResetTextWaveMent();
        }
    }

    // Straight
    private void CheckStraight(int[] sides)
    {
        bool isStraight = true;
        straightText.WobbleText();
        for (int i = 1; i < sides.Length; i++)
        {
            if (sides[i] != sides[i - 1] + 1)
            {
                isStraight = false;
                straightText.ResetTextWaveMent();
                break;
            }
        }
        straight = isStraight;
    }

    // Yacht
    private void CheckYacht(int[] sides)
    {
        if (sides[0] == sides[4])
        {
            yacht = true;
            yachtText.WobbleText();
        }
        else
        {
            yacht = false;
            yachtText.ResetTextWaveMent();
        }
    }
}

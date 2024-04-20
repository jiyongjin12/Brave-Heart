using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScore : MonoBehaviour
{
    public float DamageScore = 0;
    public bool fullHouse;
    public bool fourOfAKind;
    public bool straight;
    public bool yacht;

    private void Update()
    {
        //DiceManager�� �ν��Ͻ��� ������
        DiceManager manager = DiceManager.instance;

        // DiceManager�� �ִ� selectedDice.finalSide ���� �����ͼ� �迭�� ����
        int sum = 0;
        foreach (var die in manager.selectedDice)
        {
            if (die != null)
            {
                sum += (int)die.finalSide;
            }
        }
        DamageScore = sum;

        // DiceManager�� �ִ� diceList.finalSide ���� �����ͼ� �迭�� ����
        int[] sides = new int[manager.diceList.Count];
        for (int i = 0; i < manager.diceList.Count; i++)
        {
            sides[i] = manager.diceList[i].finalSide;
        }

        System.Array.Sort(sides);

        CheckFullHouse(sides);
        CheckFourOfAKind(sides);
        CheckStraight(sides);
        CheckYacht(sides);
    }

    // Full House ���� Ȯ��
    private void CheckFullHouse(int[] sides)
    {
        if ((sides[0] == sides[1] && sides[1] == sides[2] && sides[3] == sides[4]) ||
            (sides[0] == sides[1] && sides[2] == sides[3] && sides[3] == sides[4]))
        {
            fullHouse = true;
        }
        else
        {
            fullHouse = false;
        }
    }

    // Four of a Kind ���� Ȯ��
    private void CheckFourOfAKind(int[] sides)
    {
        if ((sides[0] == sides[1] && sides[1] == sides[2] && sides[2] == sides[3]) ||
            (sides[1] == sides[2] && sides[2] == sides[3] && sides[3] == sides[4]))
        {
            fourOfAKind = true;
        }
        else
        {
            fourOfAKind = false;
        }
    }

    // Straight ���� Ȯ��
    private void CheckStraight(int[] sides)
    {
        bool isStraight = true;
        for (int i = 1; i < sides.Length; i++)
        {
            if (sides[i] != sides[i - 1] + 1)
            {
                isStraight = false;
                break;
            }
        }
        straight = isStraight;
    }

    // Yacht ���� Ȯ��
    private void CheckYacht(int[] sides)
    {
        if (sides[0] == sides[4])
        {
            yacht = true;
        }
        else
        {
            yacht = false;
        }
    }
}

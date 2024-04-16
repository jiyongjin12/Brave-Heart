using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewBehaviourScript : MonoBehaviour
{
    public List<int> ho = new List<int> { 1, 1, 1, 5, 5 };
    bool i1;
    bool i2;
    bool i3;

    private void Update()
    {
        if(i1)
        {
            CheckFullHouse(ho);
            Debug.Log("1");
        }
        if(i2)
        {
            CheckYacht(ho);
            Debug.Log("2");
        }
        if(i3)
        {
            CheckFourOfAKind(ho);
            Debug.Log("3");
        }

    }

    //public static void Main(string[] args)
    //{
    //    List<int> Num = new List<int> { 1, 1, 1, 5, 5 };

    //    // ��� ���
    //    Debug.Log("�ֻ��� ����: " + string.Join(", ", Num));

    //    bool isFullHouse = CheckFullHouse(Num);
    //    bool isYacht = CheckYacht(Num);
    //    bool isFourOfAKind = CheckFourOfAKind(Num);

    //    Debug.Log("Full House: " + isFullHouse);
    //    Debug.Log("Yacht: " + isYacht);
    //    Debug.Log("Four of a Kind: " + isFourOfAKind);
    //}

    // Full House Ȯ�� �Լ�
    public bool CheckFullHouse(List<int> Num)
    {
        var counts = Num.GroupBy(x => x).Select(group => group.Count()).ToList();
        return counts.Contains(2) && counts.Contains(3);
    }

    // Yacht Ȯ�� �Լ�
    public bool CheckYacht(List<int> Num)
    {
        return Num.Distinct().Count() == 1;
    }

    // Four of a Kind Ȯ�� �Լ�
    public bool CheckFourOfAKind(List<int> Num)
    {
        var counts = Num.GroupBy(x => x).Select(group => group.Count()).ToList();
        return counts.Contains(4);
    }
}

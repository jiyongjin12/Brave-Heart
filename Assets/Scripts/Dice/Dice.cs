using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    //[Header("BouncingDice")]
    //[SerializeField]
    //public float maxHeight = 1.5f;      // �ִ� ����(ũ��)
    //private float height = 0f;           // ���� ����(ũ��)
    //private float acceleration = 0f;     // ���ӵ���
    //private float bouncingCount = 0f;    // ƨ��� �ִ� ��

    //[Header("RollingDice")]
    //[SerializeField] private Sprite[] diceSides; // �ֻ��� ��������Ʈ��
    //private SpriteRenderer rend;                 // ��������Ʈ������
    //private int randomDiceSides = 0;             // 1 ~ 5 ����
    //private int finalSide = 0;                   // ������ �����
    //private bool loringCheck;

    //public event Action<float> HeightChanged; // ���� ���̰� ���� �̺�Ʈ

    //private bool isHolding;
    //private bool Doubleclickprevention = true;

    //private void Start()
    //{
    //    rend = GetComponent<SpriteRenderer>();
    //}

    //private void Update()
    //{

    //    if (DiceManager.instance.isDragging)
    //    {
    //        isHolding = true;
    //    }

    //    if (isHolding)
    //    {
    //        if (Input.GetMouseButton(0) && Doubleclickprevention)
    //        {
    //            height = maxHeight;
    //            transform.localScale = Vector3.one * height;
    //            acceleration = 0;
    //            loringCheck = true;
    //        }
    //        else
    //        {
    //            Doubleclickprevention = false;

    //            acceleration -= Time.deltaTime * 0.2f; 
    //            height += acceleration;  // ���̰��� ���ӷ°� ��� ���ϱ�

    //            if (height <= .6)
    //            {
    //                // ���ӵ����� -���Ͽ� +�� ����
    //                acceleration = (acceleration / 1.12f) * -1;
    //                height = .6f;
    //                bouncingCount += 1;

    //                // �ֻ��� �� �ٲٱ�
    //                randomDiceSides = UnityEngine.Random.Range(0, 6);
    //                rend.sprite = diceSides[randomDiceSides];
    //            }



    //            if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5)
    //            {
    //                acceleration = 0f;
    //                height = .7f;
    //                bouncingCount = 0;

    //                if (loringCheck)
    //                {
    //                    finalSide = randomDiceSides + 1;

    //                    Debug.Log(finalSide);
    //                    loringCheck = false;
    //                    isHolding = false;
    //                    Doubleclickprevention = true;
    //                }
    //            }

    //            transform.localScale = Vector3.one * height;

    //            HeightChanged?.Invoke(height);
    //        }
    //    }
    //} 






    [Header("BouncingDice")]
    [SerializeField]
    public float maxHeight = 1.5f;      // �ִ� ����(ũ��)
    private float height = 0f;           // ���� ����(ũ��)
    private float acceleration = 0f;     // ���ӵ���
    private float bouncingCount = 0f;    // ƨ��� �ִ� ��

    [Header("RollingDice")]
    public Transform dice;
    [SerializeField] private Sprite[] diceSides; // �ֻ��� ��������Ʈ��
    private SpriteRenderer rend;                 // ��������Ʈ������
    private int randomDiceSides = 0;             // 1 ~ 6 ����
    private int finalSide = 0;                   // ������ �����

    public event Action<float> HeightChanged; // ���� ���̰� ���� �̺�Ʈ

    public bool W = false;
    public bool coliderCheck = false;
    private bool isHolding;
    private bool Doubleclickprevention = true; // ƨ��� ���� �ٽ� ������ ����

    public bool endLoring = true;

    private void Start()
    {
        rend = dice.GetComponent<SpriteRenderer>();
       
    }

    private void OnMouseOver()
    {
        if (W) return;

        if (Input.GetMouseButtonDown(1) && endLoring) // ��Ŭ�� �˻�
        {
            // ���⼭ DiceManager�� diceRollingList���� �ش� ������Ʈ�� �����ϴ� �ڵ� �߰�
            DiceManager.instance.diceRollingList.Remove(GetComponent<Rigidbody2D>());
            DiceManager.instance.selectedDice.Add(GetComponent<Dice>());
            W = true;
            coliderCheck = true;
        }
    }

    private void Update()
    {

        if (W) return;

        if (DiceManager.instance.isDragging)
        {
            isHolding = true;
        }

        if (isHolding)
        {
            endLoring = false;
            if (Input.GetMouseButton(0) && Doubleclickprevention) // ���콺�� Ŭ���ϰ������� && �ֻ����� ƨ��� ���� �ٽ� ��������������
            {
                height = maxHeight;
                dice.localScale = Vector3.one * height;
                acceleration = 0;
            }
            else // ���콺�� ������
            {
                Doubleclickprevention = false;

                acceleration -= Time.deltaTime * 0.2f;
                height += acceleration;  // ���̰��� ���ӷ°� ��� ���ϱ�

                if (height <= .6) // ƨ��� �̺�Ʈ
                {
                    // ���ӵ����� -���Ͽ� +�� ����
                    acceleration = (acceleration / 1.12f) * -1;
                    height = .6f;
                    bouncingCount += 1;

                    // �ֻ��� �� �ٲٱ�
                    randomDiceSides = UnityEngine.Random.Range(0, 6);
                    rend.sprite = diceSides[randomDiceSides];
                }



                if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5)  // ���ӵ����� 0�� ����� && ���̰� 0.7���� �۰ų� ���� || ƨ�� Ƚ���� 5���� ũ�ų� �������
                {
                    acceleration = 0f;
                    height = .7f;
                    bouncingCount = 0;


                    finalSide = randomDiceSides + 1;

                    Debug.Log(finalSide);
                    isHolding = false;
                    Doubleclickprevention = true;
                    endLoring = true;
                }

                dice.localScale = Vector3.one * height;

                HeightChanged?.Invoke(height);
            }
        }
    }


    //���� ��Ŭ�� �ϸ� Bool w���� DiceManager�� ���� ���⼭ ���� �ϸ� �Ǵ°�
    //���� ũ�Ⱑ ���۾������� ��Ŭ���̳� �ٽ� Ű��⳪ �ٽ� ���� ���� �Ҽ��ִٸ� �������
}
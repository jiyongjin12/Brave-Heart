using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    [Header("BouncingDice")]
    [SerializeField]
    public float maxHeight = 1.5f;      // �ִ� ����(ũ��)
    private float height = 0f;           // ���� ����(ũ��)
    private float acceleration = 0f;     // ���ӵ���
    private float bouncingCount = 0f;    // ƨ��� �ִ� ��

    [Header("RollingDice")]
    [SerializeField] private Sprite[] diceSides; // �ֻ��� ��������Ʈ��
    private SpriteRenderer rend;                 // ��������Ʈ������
    private int randomDiceSides = 0;             // 1 ~ 5 ����
    private int finalSide = 0;                   // ������ �����
    private bool loringCheck;

    public event Action<float> HeightChanged; // ���� ���̰� ���� �̺�Ʈ

    private bool isHolding;
    private bool Doubleclickprevention = true;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        if (DiceManager.instance.isDragging)
        {
            isHolding = true;
        }

        if (isHolding)
        {
            if (Input.GetMouseButton(0) && Doubleclickprevention)
            {
                height = maxHeight;
                transform.localScale = Vector3.one * height;
                acceleration = 0;
                loringCheck = true;
            }
            else
            {
                Doubleclickprevention = false;

                acceleration -= Time.deltaTime * 0.2f; 
                height += acceleration;  // ���̰��� ���ӷ°� ��� ���ϱ�

                if (height <= .6)
                {
                    // ���ӵ����� -���Ͽ� +�� ����
                    acceleration = (acceleration / 1.12f) * -1;
                    height = .6f;
                    bouncingCount += 1;

                    // �ֻ��� �� �ٲٱ�
                    randomDiceSides = UnityEngine.Random.Range(0, 6);
                    rend.sprite = diceSides[randomDiceSides];
                }



                if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5)
                {
                    acceleration = 0f;
                    height = .7f;
                    bouncingCount = 0;

                    if (loringCheck)
                    {
                        finalSide = randomDiceSides + 1;

                        Debug.Log(finalSide);
                        loringCheck = false;
                        isHolding = false;
                        Doubleclickprevention = true;
                    }
                }

                transform.localScale = Vector3.one * height;

                HeightChanged?.Invoke(height);
            }
        }
    } 
}
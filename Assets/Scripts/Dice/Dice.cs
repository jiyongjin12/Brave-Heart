using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    [Header("BouncingDice")]
    [SerializeField]
    public float maxHeight = 1.5f;      // 최대 높이(크기)
    private float height = 0f;           // 현재 높이(크기)
    private float acceleration = 0f;     // 가속도값
    private float bouncingCount = 0f;    // 튕기는 최대 값

    [Header("RollingDice")]
    [SerializeField] private Sprite[] diceSides; // 주사위 스프라이트들
    private SpriteRenderer rend;                 // 스프라이트렌더러
    private int randomDiceSides = 0;             // 1 ~ 5 랜덤
    private int finalSide = 0;                   // 마지막 결과값
    private bool loringCheck;

    public event Action<float> HeightChanged; // 현재 높이값 변경 이벤트

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
                height += acceleration;  // 높이값에 가속력값 계속 더하기

                if (height <= .6)
                {
                    // 가속도값에 -곱하여 +로 만듬
                    acceleration = (acceleration / 1.12f) * -1;
                    height = .6f;
                    bouncingCount += 1;

                    // 주사위 눈 바꾸기
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
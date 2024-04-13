using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    //[Header("BouncingDice")]
    //[SerializeField]
    //public float maxHeight = 1.5f;      // 최대 높이(크기)
    //private float height = 0f;           // 현재 높이(크기)
    //private float acceleration = 0f;     // 가속도값
    //private float bouncingCount = 0f;    // 튕기는 최대 값

    //[Header("RollingDice")]
    //[SerializeField] private Sprite[] diceSides; // 주사위 스프라이트들
    //private SpriteRenderer rend;                 // 스프라이트렌더러
    //private int randomDiceSides = 0;             // 1 ~ 5 랜덤
    //private int finalSide = 0;                   // 마지막 결과값
    //private bool loringCheck;

    //public event Action<float> HeightChanged; // 현재 높이값 변경 이벤트

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
    //            height += acceleration;  // 높이값에 가속력값 계속 더하기

    //            if (height <= .6)
    //            {
    //                // 가속도값에 -곱하여 +로 만듬
    //                acceleration = (acceleration / 1.12f) * -1;
    //                height = .6f;
    //                bouncingCount += 1;

    //                // 주사위 눈 바꾸기
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
    public float maxHeight = 1.5f;      // 최대 높이(크기)
    private float height = 0f;           // 현재 높이(크기)
    private float acceleration = 0f;     // 가속도값
    private float bouncingCount = 0f;    // 튕기는 최대 값

    [Header("RollingDice")]
    public Transform dice;
    [SerializeField] private Sprite[] diceSides; // 주사위 스프라이트들
    private SpriteRenderer rend;                 // 스프라이트렌더러
    private int randomDiceSides = 0;             // 1 ~ 6 랜덤
    private int finalSide = 0;                   // 마지막 결과값

    public event Action<float> HeightChanged; // 현재 높이값 변경 이벤트

    public bool W = false;
    public bool coliderCheck = false;
    private bool isHolding;
    private bool Doubleclickprevention = true; // 튕기는 도중 다시 잡을수 없게

    public bool endLoring = true;

    private void Start()
    {
        rend = dice.GetComponent<SpriteRenderer>();
       
    }

    private void OnMouseOver()
    {
        if (W) return;

        if (Input.GetMouseButtonDown(1) && endLoring) // 우클릭 검사
        {
            // 여기서 DiceManager의 diceRollingList에서 해당 오브젝트를 제거하는 코드 추가
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
            if (Input.GetMouseButton(0) && Doubleclickprevention) // 마우스를 클릭하고있을때 && 주사위가 튕기던 도중 다시 잡을수없도록함
            {
                height = maxHeight;
                dice.localScale = Vector3.one * height;
                acceleration = 0;
            }
            else // 마우스를 땠을때
            {
                Doubleclickprevention = false;

                acceleration -= Time.deltaTime * 0.2f;
                height += acceleration;  // 높이값에 가속력값 계속 더하기

                if (height <= .6) // 튕기는 이벤트
                {
                    // 가속도값에 -곱하여 +로 만듬
                    acceleration = (acceleration / 1.12f) * -1;
                    height = .6f;
                    bouncingCount += 1;

                    // 주사위 눈 바꾸기
                    randomDiceSides = UnityEngine.Random.Range(0, 6);
                    rend.sprite = diceSides[randomDiceSides];
                }



                if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5)  // 가속도값이 0에 가까움 && 높이가 0.7보다 작거나 같음 || 튕긴 횟수가 5보다 크거나 같을경우
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


    //이제 우클릭 하면 Bool w값과 DiceManager의 값을 여기서 빼고 하면 되는거
    //이제 크기가 다작아져야지 우클릭이나 다시 키우기나 다시 시작 등을 할수있다를 넣으면됨
}
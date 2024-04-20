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
    public Transform dice;
    public Sprite[] diceSides; // 주사위 스프라이트들
    [HideInInspector]
    public SpriteRenderer rend;                 // 스프라이트렌더러
    public int randomDiceSides = 0;             // 1 ~ 6 랜덤
    public int finalSide = 0;                   // 마지막 결과값

    public event Action<float> HeightChanged; // 현재 높이값 변경 이벤트

    public bool SelectedDice = false;
    public bool BounceTime = false; // 드레그중 손 땠을때 부터 바운스가 멈출때까지
    private bool isHolding;

    public bool endLoring = true;
    //public bool reRollCheck = false;

    private void Start()
    {
        rend = dice.GetComponent<SpriteRenderer>();

    }

    private void OnMouseOver()
    {
        if (SelectedDice) return;

        if (Input.GetMouseButtonDown(1) && endLoring) // 우클릭 검사
        {
            // 여기서 DiceManager의 diceRollingList에서 해당 오브젝트를 제거하는 코드
            DiceManager.instance.diceRollingList.Remove(GetComponent<Rigidbody2D>());
            DiceManager.instance.diceEventCheckList.Remove(GetComponent<Dice>());
            DiceManager.instance.selectedDice.Add(GetComponent<Dice>());
            SelectedDice = true;
        }
    }

    private void Update()
    {

        if (SelectedDice) return;

        if (DiceManager.instance.isDragging)
        {
            isHolding = true;
        }

        StartCoroutine(RollingDice());

    }

    private IEnumerator RollingDice()
    {
        yield return new WaitUntil(() => isHolding);

        endLoring = false;
        if (Input.GetMouseButton(0) && !BounceTime) // 마우스를 클릭하고있을때 && 주사위가 튕기던 도중 다시 잡을수없도록함
        {
            height = maxHeight;
            dice.localScale = Vector3.one * height;
            acceleration = 0;
        }
        else // 마우스를 땠을때
        {
            BounceTime = true;

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
                BounceTime = false;
                endLoring = true;
                
            }
            dice.localScale = Vector3.one * height;

            HeightChanged?.Invoke(height);
        }
    }

}
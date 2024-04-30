using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public List<Dice> diceList = new List<Dice>();   // 턴 엔드나 다시 돌릴거 없을때 여기꺼 받아오면 되쟈나? 나 ㅄ인가?
    public List<Rigidbody2D> diceRollingList = new List<Rigidbody2D>();   //
    public List<Dice> selectedDice = new List<Dice>();                    // 우클릭시 이 두 리스트의 관계는 Dice.cs에 있음
    public List<Dice> diceEventCheckList = new List<Dice>(); // 코드 재사용성 안좋음(아마)

    public int rerollNum = 4;
    public TMP_Text RollingNumText;
    private Vector3 targetPosition;
    public bool isDragging = false;
    private float moveSpeed = 30f;
    private float throwSpeed = 38f;
    private float deceleration = 1.7f; // 감속도

    public bool isRollingDice = true;

    public static DiceManager instance { get; private set; }


    private void Awake()
    {
        instance = this;

        foreach(var n in diceList)
        {
            diceRollingList.Add(n.GetComponent<Rigidbody2D>());
            diceEventCheckList.Add(n.GetComponent<Dice>());
  
        }
    }

    private void Update()
    {
        RollingNumText.text = "X " + rerollNum.ToString();
        dragUpDice();

        if (isRollingDice)
        {

            if (Input.GetMouseButton(0))
            {
                isDragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                RollingDice();
                isRollingDice = false;
                rerollNum -= 1;
            }
        }

        if (isDragging)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;
            foreach (Rigidbody2D rb in diceRollingList)
            {
                Vector3 moveDirection = (targetPosition - rb.transform.position).normalized;
                rb.velocity = moveDirection * moveSpeed;
            }
        }
        else
        {
            // 속도감속
            foreach (Rigidbody2D rb in diceRollingList)
            {
                rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;
                rb.angularVelocity *= Mathf.Clamp01(1f - deceleration * Time.deltaTime);
            }
        }

        if (diceEventCheckList.Count != 0) // diceEventCheckList가 0이되서 오류나는것을 방지
        {
            if (rerollNum > 0 && !diceEventCheckList[0].BounceTime)
            {
                if (diceEventCheckList[0].endLoring && !isRollingDice)
                {
                    isRollingDice = true;
                    //rerollNum -= 1;
                }
            }

            if (rerollNum <= 0 && diceEventCheckList[0].endLoring) // 여기 dice.endLoring으로 추가 해주면 될듯? R말고 다시 시작이 있다면
            {
                StartCoroutine(DiceEndMove());
            }
        }
        
        

        if (Input.GetKeyDown(KeyCode.L)) // 초기화 버튼 (없앨예정)
        {
            IsMyTurn();
        }
    }

    private void RollingDice()
    {
        foreach (Rigidbody2D dice in diceRollingList)
        {
            dice.velocity = Vector2.zero;
            dice.angularVelocity = Random.Range(-360f, 360f);
            dice.AddForce(Random.insideUnitCircle * throwSpeed, ForceMode2D.Impulse);
        }
    }


    private void IsMyTurn()
    {
        diceRollingList.Clear(); // L 연타시 늘어나는거 방지 , 나중에 없에야 할것
        diceEventCheckList.Clear(); // L 연타시 늘어나는거 방지 , 나중에 없에야 할것
        selectedDice.Clear();
        rerollNum = 4;

        for (int n = 0; n < diceList.Count; n++)
        {
            Vector3 StartDicePos = Vector3.zero;
            Quaternion targetRotation = Quaternion.identity;
            float StartDiceScale = 1.2f;

            switch (n)
            {
                case 0:
                    StartDicePos = new Vector3(-3.32f, -1.65f, 0f);
                    break;
                case 1:
                    StartDicePos = new Vector3(-1.68f, -1.65f, 0f);
                    break;
                case 2:
                    StartDicePos = new Vector3(-0.04f, -1.65f, 0f);
                    break;
                case 3:
                    StartDicePos = new Vector3(1.58f, -1.65f, 0f);
                    break;
                case 4:
                    StartDicePos = new Vector3(3.22f, -1.65f, 0f);
                    break;
            }

            diceList[n].transform.position = StartDicePos; // 초반 위치
            diceList[n].transform.rotation = targetRotation; // 초반 회전도
            diceList[n].dice.transform.localScale = Vector3.one * StartDiceScale; // 초반 크기

            diceList[n].dice.GetComponent<SpriteRenderer>().sprite = diceList[n].diceSides[0]; //오브젝트 1로 바꾸기
            diceList[n].endLoring = true;
        }

        foreach (var n in diceList)
        {
            diceRollingList.Add(n.GetComponent<Rigidbody2D>());
            diceEventCheckList.Add(n.GetComponent<Dice>());
            for (int i = 0; i < 5; i++)
            {
                diceList[i].SelectedDice = false;
            }
        }
    }

    private void dragUpDice()
    {
        for (int i = 0; i < selectedDice.Count; i++)
        {
            Vector3 targetPosition = Vector3.zero;
            Quaternion targetRotation = Quaternion.identity;
            float targetScale = 1.2f; // 목표 크기

            // 각 주사위 별로 목표 위치, 회전, 크기 설정
            switch (i)
            {
                case 0:
                    targetPosition = new Vector3(-3.32f, 2.36f, 0f);
                    break;
                case 1:
                    targetPosition = new Vector3(-1.68f, 2.36f, 0f);
                    break;
                case 2:
                    targetPosition = new Vector3(-0.04f, 2.36f, 0f);
                    break;
                case 3:
                    targetPosition = new Vector3(1.58f, 2.36f, 0f);
                    break;
                case 4:
                    targetPosition = new Vector3(3.22f, 2.36f, 0f);
                    break;
            }

            // 주사위 이동
            selectedDice[i].transform.position = Vector3.MoveTowards(selectedDice[i].transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 주사위 크기 조절
            selectedDice[i].dice.transform.localScale = Vector3.one * Mathf.MoveTowards(selectedDice[i].dice.transform.localScale.x, targetScale, moveSpeed * Time.deltaTime);

            // 주사위 회전
            selectedDice[i].transform.rotation = Quaternion.RotateTowards(selectedDice[i].transform.rotation, targetRotation, moveSpeed * Time.deltaTime * 30);

            selectedDice[i].SelectedDice = true;

            //Debug.Log(selectedDice[i].finalSide);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            foreach (Rigidbody2D rb in diceRollingList)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private IEnumerator DiceEndMove()
    {
        foreach (Dice dice in diceList)
        {
            if (!selectedDice.Contains(dice)) // 이미 선택된 주사위는 제외하고 선택된 주사위로 추가
            {
                selectedDice.Add(dice);
                yield return new WaitForSeconds(.1f);
            }
        }

        // diceRollingList를 비웁니다.
        diceRollingList.Clear();
        yield return null;
    }
}

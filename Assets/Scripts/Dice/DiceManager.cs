using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //public List<Rigidbody2D> diceRollingList = new List<Rigidbody2D>();

    //private Vector3 targetPosition;
    //public bool isDragging = false;
    //private float moveSpeed = 20f;
    //private float throwSpeed = 35f;
    //private float deceleration = 1.7f; // 감속도

    //public bool isRollingDice = true;

    //public static DiceManager instance { get; private set; }

    //private void Awake()
    //{
    //    instance = this;
    //}

    //private void Update()
    //{
    //    if (isRollingDice)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            isDragging = true;
    //        }

    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            isDragging = false;
    //            RollingDice();
    //            isRollingDice = false;
    //        }
    //    }

    //    if (isDragging)
    //    {
    //        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        targetPosition.z = 0f;
    //        foreach (Rigidbody2D rb in diceRollingList)
    //        {
    //            Vector3 moveDirection = (targetPosition - rb.transform.position).normalized;
    //            rb.velocity = moveDirection * moveSpeed;
    //        }
    //    }
    //    else
    //    {
    //        // 속도감속
    //        foreach (Rigidbody2D rb in diceRollingList)
    //        {
    //            rb.velocity -= rb.velocity.normalized * deceleration * Time.deltaTime;
    //            rb.angularVelocity *= Mathf.Clamp01(1f - deceleration * Time.deltaTime);
    //        }
    //    }

    //    if(Input.GetKeyDown(KeyCode.R) && !isRollingDice)
    //    {
    //        isRollingDice = true;
    //    }
    //}

    //private void RollingDice()
    //{
    //    foreach (Rigidbody2D dice in diceRollingList)
    //    {
    //        dice.velocity = Vector2.zero;
    //        dice.angularVelocity = Random.Range(-360f, 360f);
    //        dice.AddForce(Random.insideUnitCircle * throwSpeed, ForceMode2D.Impulse);
    //    }
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (isDragging)
    //    {
    //        foreach (Rigidbody2D rb in diceRollingList)
    //        {
    //            rb.velocity = Vector2.zero;
    //        }
    //    }
    //}







    public List<Dice> diceList = new List<Dice>();
    [SerializeField]
    public List<Rigidbody2D> diceRollingList = new List<Rigidbody2D>();

    public List<Dice> selectedDice = new List<Dice>();

    public int rerollNum = 4; 
    private Vector3 targetPosition;
    public bool isDragging = false;
    private float moveSpeed = 20f;
    private float throwSpeed = 35f;
    private float deceleration = 1.7f; // 감속도

    public bool isRollingDice = true;
    public bool isSelectedDice = false;

    public static DiceManager instance { get; private set; }

    private void Awake()
    {
        instance = this;

        foreach(var n in diceList)
        {
            diceRollingList.Add(n.GetComponent<Rigidbody2D>());
        }
    }

    private void Update()
    {
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

        if (rerollNum > 0)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isRollingDice)
            {
                isRollingDice = true;
                rerollNum -= 1;
            }
        } 

        if (Input.GetKeyDown(KeyCode.L))
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
        diceRollingList.Clear();

        foreach (var n in diceList)
        {
            diceRollingList.Add(n.GetComponent<Rigidbody2D>());
            for (int i = 0; i < 5; i++)
            {
                diceList[i].W = false;
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
                    targetPosition = new Vector3(-3.25f, 2.36f, 0f);
                    break;
                case 1:
                    targetPosition = new Vector3(-1.65f, 2.36f, 0f);
                    break;
                case 2:
                    targetPosition = new Vector3(0f, 2.36f, 0f);
                    break;
                case 3:
                    targetPosition = new Vector3(1.65f, 2.36f, 0f);
                    break;
                case 4:
                    targetPosition = new Vector3(3.25f, 2.36f, 0f);
                    break;
            }

            // 주사위 이동
            selectedDice[i].transform.position = Vector3.MoveTowards(selectedDice[i].transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 주사위 크기 조절
            selectedDice[i].dice.transform.localScale = Vector3.one * Mathf.MoveTowards(selectedDice[i].transform.localScale.x, targetScale, moveSpeed * Time.deltaTime);

            // 주사위 회전
            selectedDice[i].transform.rotation = Quaternion.RotateTowards(selectedDice[i].transform.rotation, targetRotation, moveSpeed * Time.deltaTime * 30);

            selectedDice[i].W = true;
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
}

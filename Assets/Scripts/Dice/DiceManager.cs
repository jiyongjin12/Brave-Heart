using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public List<Rigidbody2D> diceRollingList = new List<Rigidbody2D>();

    private Vector3 targetPosition;
    public bool isDragging = false;
    private float moveSpeed = 20f;
    private float throwSpeed = 35f;
    private float deceleration = 1.7f; // 감속도

    public bool isRollingDice = true;

    public static DiceManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
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

        if(Input.GetKeyDown(KeyCode.R) && !isRollingDice)
        {
            isRollingDice = true;
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

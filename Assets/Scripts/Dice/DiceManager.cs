using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public List<Rigidbody2D> diceList = new List<Rigidbody2D>();

    private Vector3 targetPosition;
    private bool isDragging = false;
    private float moveSpeed = 20f;

    private float throwSpeed = 35f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            RollingDice();
        }

        if (isDragging)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;
            foreach (Rigidbody2D rb in diceList)
            {
                Vector3 moveDirection = (targetPosition - rb.transform.position).normalized;
                rb.velocity = moveDirection * moveSpeed;
            }
        }
        else
        {

        }
    }
    
    private void RollingDice()
    {
        foreach (Rigidbody2D dice in diceList)
        {
            dice.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            dice.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-360f, 360f);
            dice.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * throwSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDragging)
        {
            foreach (Rigidbody2D rb in diceList)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float moveSpeed = 20f; // 주사위 이동 속도
    public float explosionForce = 100f; // 폭발 힘

    private bool isMoving = false;
    private Vector3 targetPosition;
    public List<GameObject> diceList = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치로 주사위 이동 시작
            isMoving = true;
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // 카메라와의 거리를 조정
        }

        if (isMoving)
        {
            // 각 주사위를 마우스 위치로 이동
            foreach (GameObject dice in diceList)
            {
                if (dice.GetComponent<Rigidbody2D>().velocity.magnitude == 0f)
                {
                    // 주사위가 멈춰있는 경우에만 이동 처리
                    Vector3 direction = targetPosition - dice.transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(dice.transform.position, direction.normalized, direction.magnitude);

                    if (hit.collider != null)
                    {
                        // 이동 경로에 Collider가 있으면 이동하지 않음
                        continue;
                    }

                    dice.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // 마우스를 뗄 때 주사위를 회전하며 폭발
            foreach (GameObject dice in diceList)
            {
                dice.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                dice.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-360f, 360f);
                dice.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * explosionForce, ForceMode2D.Impulse);
            }
        }
    }
}
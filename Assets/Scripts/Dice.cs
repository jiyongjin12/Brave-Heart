using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float moveSpeed = 20f; // �ֻ��� �̵� �ӵ�
    public float explosionForce = 100f; // ���� ��

    private bool isMoving = false;
    private Vector3 targetPosition;
    public List<GameObject> diceList = new List<GameObject>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ�� �ֻ��� �̵� ����
            isMoving = true;
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // ī�޶���� �Ÿ��� ����
        }

        if (isMoving)
        {
            // �� �ֻ����� ���콺 ��ġ�� �̵�
            foreach (GameObject dice in diceList)
            {
                if (dice.GetComponent<Rigidbody2D>().velocity.magnitude == 0f)
                {
                    // �ֻ����� �����ִ� ��쿡�� �̵� ó��
                    Vector3 direction = targetPosition - dice.transform.position;
                    RaycastHit2D hit = Physics2D.Raycast(dice.transform.position, direction.normalized, direction.magnitude);

                    if (hit.collider != null)
                    {
                        // �̵� ��ο� Collider�� ������ �̵����� ����
                        continue;
                    }

                    dice.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // ���콺�� �� �� �ֻ����� ȸ���ϸ� ����
            foreach (GameObject dice in diceList)
            {
                dice.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                dice.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-360f, 360f);
                dice.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * explosionForce, ForceMode2D.Impulse);
            }
        }
    }
}
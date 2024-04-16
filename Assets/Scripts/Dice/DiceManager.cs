using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public List<Dice> diceList = new List<Dice>();   // �� ���峪 �ٽ� ������ ������ ���Ⲩ �޾ƿ��� ����? �� ���ΰ�?
    public List<Rigidbody2D> diceRollingList = new List<Rigidbody2D>();   //
    public List<Dice> selectedDice = new List<Dice>();                    // ��Ŭ���� �� �� ����Ʈ�� ����� Dice.cs�� ����

    public int rerollNum = 4;
    public TMP_Text RollingNumText;
    private Vector3 targetPosition;
    public bool isDragging = false;
    private float moveSpeed = 30f;
    private float throwSpeed = 38f;
    private float deceleration = 1.7f; // ���ӵ�

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
            // �ӵ�����
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

        //if (rerollNum <= 0) // �ڷ�ƾ���� �ٲܸ���
        //{
        //    StartCoroutine(DiceEndMove());
        //}

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
                    StartDicePos = new Vector3(-3.32f, -2, 0f);
                    break;
                case 1:
                    StartDicePos = new Vector3(-1.68f, -2, 0f);
                    break;
                case 2:
                    StartDicePos = new Vector3(-0.04f, -2, 0f);
                    break;
                case 3:
                    StartDicePos = new Vector3(1.58f, -2, 0f);
                    break;
                case 4:
                    StartDicePos = new Vector3(3.22f, -2, 0f);
                    break;
            }

            diceList[n].transform.position = StartDicePos; // �ʹ� ��ġ
            diceList[n].transform.rotation = targetRotation; // �ʹ� ȸ����
            diceList[n].dice.transform.localScale = Vector3.one * StartDiceScale; // �ʹ� ũ��

            diceList[n].dice.GetComponent<SpriteRenderer>().sprite = diceList[n].diceSides[0]; //������Ʈ 1�� �ٲٱ�
            diceList[n].endLoring = false;
        }

        foreach (var n in diceList)
        {
            diceRollingList.Add(n.GetComponent<Rigidbody2D>());
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
            float targetScale = 1.2f; // ��ǥ ũ��

            // �� �ֻ��� ���� ��ǥ ��ġ, ȸ��, ũ�� ����
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

            // �ֻ��� �̵�
            selectedDice[i].transform.position = Vector3.MoveTowards(selectedDice[i].transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // �ֻ��� ũ�� ����
            selectedDice[i].dice.transform.localScale = Vector3.one * Mathf.MoveTowards(selectedDice[i].dice.transform.localScale.x, targetScale, moveSpeed * Time.deltaTime);

            // �ֻ��� ȸ��
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
            if (!dice.endLoring) // ���õ��� ���� �ֻ����� ���õ� �ֻ����� �̵�
            {
                selectedDice.Add(dice);
                dice.endLoring = true;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{
    [Header("BouncingDice")]
    [SerializeField]
    public float maxHeight = 1.5f;      // �ִ� ����(ũ��)
    private float height = 0f;           // ���� ����(ũ��)
    private float acceleration = 0f;     // ���ӵ���
    private float bouncingCount = 0f;    // ƨ��� �ִ� ��

    [Header("RollingDice")]
    public Transform dice;
    public Sprite[] diceSides; // �ֻ��� ��������Ʈ��
    [HideInInspector]
    public SpriteRenderer rend;                 // ��������Ʈ������
    public int randomDiceSides = 0;             // 1 ~ 6 ����
    public int finalSide = 0;                   // ������ �����

    public event Action<float> HeightChanged; // ���� ���̰� ���� �̺�Ʈ

    public bool SelectedDice = false;
    public bool BounceTime = false; // �巹���� �� ������ ���� �ٿ�� ���⶧����
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

        if (Input.GetMouseButtonDown(1) && endLoring) // ��Ŭ�� �˻�
        {
            // ���⼭ DiceManager�� diceRollingList���� �ش� ������Ʈ�� �����ϴ� �ڵ�
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
        if (Input.GetMouseButton(0) && !BounceTime) // ���콺�� Ŭ���ϰ������� && �ֻ����� ƨ��� ���� �ٽ� ��������������
        {
            height = maxHeight;
            dice.localScale = Vector3.one * height;
            acceleration = 0;
        }
        else // ���콺�� ������
        {
            BounceTime = true;

            acceleration -= Time.deltaTime * 0.2f;
            height += acceleration;  // ���̰��� ���ӷ°� ��� ���ϱ�

            if (height <= .6) // ƨ��� �̺�Ʈ
            {
                // ���ӵ����� -���Ͽ� +�� ����
                acceleration = (acceleration / 1.12f) * -1;
                height = .6f;
                bouncingCount += 1;

                // �ֻ��� �� �ٲٱ�
                randomDiceSides = UnityEngine.Random.Range(0, 6);
                rend.sprite = diceSides[randomDiceSides];
            }



            if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5)  // ���ӵ����� 0�� ����� && ���̰� 0.7���� �۰ų� ���� || ƨ�� Ƚ���� 5���� ũ�ų� �������
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
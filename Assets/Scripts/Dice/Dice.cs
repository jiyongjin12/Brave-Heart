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

    [SerializeField]
    private bool DiceRotationTime = false;
    public Sprite[] diceRoll;
    private bool Rolling_test = true;

    private void Start()
    {
        rend = dice.GetComponent<SpriteRenderer>();

    }

    private void OnMouseOver()
    {
        if (SelectedDice) return;

        if (DiceManager.instance.StartSetUpBool == true)
        {
            if (Input.GetMouseButtonDown(1) && endLoring) // ��Ŭ�� �˻�
            {
                // ���⼭ DiceManager�� diceRollingList���� �ش� ������Ʈ�� �����ϴ� �ڵ�
                DiceManager.instance.diceRollingList.Remove(GetComponent<Rigidbody2D>());
                DiceManager.instance.diceEventCheckList.Remove(GetComponent<Dice>());
                DiceManager.instance.selectedDice.Add(GetComponent<Dice>());
                SelectedDice = true;
            }
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

        if (endLoring == true)
        {
            DiceRotationTime = false;
        }

        if (DiceRotationTime && Rolling_test)
        {
            StartCoroutine(DiceSpinningAnimation());
            Rolling_test = false;
        }
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

            acceleration -= Time.deltaTime * 0.3f;
            height += acceleration;  // ���̰��� ���ӷ°� ��� ���ϱ�

            if (height <= .6) // ƨ��� �̺�Ʈ
            {
                // ���ӵ����� -���Ͽ� +�� ����
                acceleration = (acceleration / 1.1f) * -1;
                height = .6f;
                bouncingCount += 1;

                // �ֻ��� �� �ٲٱ�
                randomDiceSides = UnityEngine.Random.Range(0, 6);
                rend.sprite = diceSides[randomDiceSides];
            }

            
            if (acceleration <= 0.0001 &&endLoring == false) // �ֻ��� ����� ��Ÿ����
            {
                DiceRotationTime = true;
                Debug.Log("Ȯ");
            }
            else
            {
                DiceRotationTime = false;
            }

            if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 6)  // ���ӵ����� 0�� ����� && ���̰� 0.7���� �۰ų� ���� || ƨ�� Ƚ���� 5���� ũ�ų� �������
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

            Debug.Log(acceleration);
        }
    }

    private IEnumerator DiceSpinningAnimation()
    {
        int randomRollIndex = UnityEngine.Random.Range(0, diceRoll.Length);
        rend.sprite = diceRoll[randomRollIndex];
        yield return new WaitForSeconds(0.3f);
        Rolling_test = true;
    }
}
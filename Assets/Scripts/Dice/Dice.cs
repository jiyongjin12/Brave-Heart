using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour
{

    [Header("BouncingDice")]
    [SerializeField]
    private float maxHeight = 1.5f;      // �ִ� ����(ũ��)
    private float height = 0f;           // ���� ����(ũ��)
    private float acceleration = 0f;     // ���ӵ���
    private float bouncingCount = 0f;    // ƨ��� �ִ� ��

    private void Start()
    {
        height = maxHeight;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            height = maxHeight;

            transform.localScale = Vector3.one * height;

            acceleration = 0;
        }
        else
        {
            acceleration -= Time.deltaTime * 0.2f; // ����� ���ӵ� ��

            height += acceleration;

            if (height <= .6)
            {
                // ���ӵ����� -���Ͽ� +�� ����
                acceleration = (acceleration / 1.12f) * -1;
                height = .6f;
                bouncingCount += 1;
                Debug.Log(bouncingCount);
            }

            if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5) // ����� ��
            {
                acceleration = 0f;
                height = .7f;
                bouncingCount = 0;
            }

            transform.localScale = Vector3.one * height;

        }
    }

}
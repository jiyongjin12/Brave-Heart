using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour
{

    [Header("BouncingDice")]
    [SerializeField]
    private float maxHeight = 1.5f;      // 최대 높이(크기)
    private float height = 0f;           // 현재 높이(크기)
    private float acceleration = 0f;     // 가속도값
    private float bouncingCount = 0f;    // 튕기는 최대 값

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
            acceleration -= Time.deltaTime * 0.2f; // 변경된 가속도 값

            height += acceleration;

            if (height <= .6)
            {
                // 가속도값에 -곱하여 +로 만듬
                acceleration = (acceleration / 1.12f) * -1;
                height = .6f;
                bouncingCount += 1;
                Debug.Log(bouncingCount);
            }

            if (Mathf.Abs(acceleration) <= 0.013f && Mathf.Abs(height) <= .7f || bouncingCount >= 5) // 변경된 값
            {
                acceleration = 0f;
                height = .7f;
                bouncingCount = 0;
            }

            transform.localScale = Vector3.one * height;

        }
    }

}
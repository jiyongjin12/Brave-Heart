using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShadow : MonoBehaviour
{
    //public GameObject cube;
    //private Vector3 initialScale;
    //private Vector3 initialPosition;
    //private float minScale = 0.7f;
    //private float moveRatioPerUnit = 0.10875f;

    //void Start()
    //{
    //    initialScale = cube.transform.localScale;
    //    initialPosition = transform.position;
    //}

    //void Update()
    //{


    //    // ť���� ���� ũ�� ���
    //    float currentScale = cube.transform.localScale.y;

    //    // ũ�� ��ȭ�� ���
    //    float scaleDifference = currentScale - initialScale.y;

    //    // �̵� �Ÿ� ���
    //    float moveDistance = scaleDifference * moveRatioPerUnit;

    //    // �ּ� ũ�� �̻��� ��쿡�� �̵�
    //    if (currentScale >= minScale)
    //    {
    //        // ���� ��ġ�� �̵� �Ÿ��� ���Ͽ� �� ��ġ ���
    //        Vector3 newPosition = initialPosition + new Vector3(-1, -1, 0) * moveDistance;

    //        // �� ��ġ�� �̵�
    //        transform.position = newPosition;
    //    }

    //    Debug.Log("�̵� �Ÿ�: " + moveDistance);
    //}

    public GameObject cube;

    private void Update()
    {
        transform.rotation = cube.transform.rotation;
        transform.localScale = cube.transform.localScale;
    }
}
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


    //    // 큐브의 현재 크기 계산
    //    float currentScale = cube.transform.localScale.y;

    //    // 크기 변화량 계산
    //    float scaleDifference = currentScale - initialScale.y;

    //    // 이동 거리 계산
    //    float moveDistance = scaleDifference * moveRatioPerUnit;

    //    // 최소 크기 이상인 경우에만 이동
    //    if (currentScale >= minScale)
    //    {
    //        // 현재 위치에 이동 거리를 더하여 새 위치 계산
    //        Vector3 newPosition = initialPosition + new Vector3(-1, -1, 0) * moveDistance;

    //        // 새 위치로 이동
    //        transform.position = newPosition;
    //    }

    //    Debug.Log("이동 거리: " + moveDistance);
    //}

    public GameObject cube;

    private void Update()
    {
        transform.rotation = cube.transform.rotation;
        transform.localScale = cube.transform.localScale;
    }
}
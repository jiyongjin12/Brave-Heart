using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPos : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.up;
    private Transform targetTransform;
    private RectTransform rectTransform;

    private void Start()
    {
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    public void SetUp(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPos = new Vector3(targetTransform.position.x, targetTransform.transform.position.y + 1.5f);

        rectTransform.position = screenPos;
    }
}

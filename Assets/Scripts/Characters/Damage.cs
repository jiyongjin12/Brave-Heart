using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private float timer;
    public Text TextDamage;
    private Transform targetTransform;
    private RectTransform rectTransform;

    private void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.position = targetTransform.position;
    }
    private void Update()
    {
        FloatDamage();
        if (timer >= 1) DestroyEvent();
        else Move();
    }

    public void FloatDamage()
    {
        float curDamage;
        curDamage = float.Parse(Unit.instance.CurDamage.ToString("N2"));
        TextDamage.text = curDamage.ToString();
    }

    public void SetUp(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Move()
    {
        timer += Time.deltaTime;
        transform.position += Vector3.up / 100;
    }

    public void DestroyEvent()
    {
        Destroy(gameObject);
    }
}

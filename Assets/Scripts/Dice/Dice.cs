using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour
{
    [SerializeField]
    private float maxScale = 1.2f;
    [SerializeField]
    private float minScale = .5f;
    [SerializeField]
    //private float durationPart1 = .5f;

    private float shrinkSpeed = .15f;
    private float w;

    private void Start()
    {
        w = maxScale;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SizeUP();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            BounceDice();
            w = maxScale;
        }
    }

    private void SizeUP()
    {
        transform.localScale = Vector3.one * minScale;
        transform.DOScale(maxScale, 0.05f);
    }

    private void BounceDice()
    {
        transform.DOScale(minScale, shrinkSpeed).OnComplete(() =>
        {
            w /= 1.18f;
            if (w > 0.6f)
            {
                transform.DOScale(w, shrinkSpeed).OnComplete(() =>
                {
                    BounceDice();
                });
            }
        });
    }
}
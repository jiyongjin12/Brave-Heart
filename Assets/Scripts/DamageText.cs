using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float time;
    private void Awake()
    {
        DmgTextText();
    }

    void Update()
    {
        
        transform.position += Vector3.up;
        if (time >= 1) Destroy(gameObject);
        else time += Time.deltaTime;
    }

    void DmgTextText()
    {
        GameManager.instance.Damage.GetComponent<TMP_Text>().text = GameManager.instance.playerDamage.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolllingDice : MonoBehaviour
{
    [SerializeField]private Sprite[] diceSides;

    private SpriteRenderer rend;

    private int randomDiceSides = 0;
    private int finalSide = 0;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StartCoroutine("RollTheDice");
        }


    }

    private IEnumerator RollTheDice()
    {
        

        if (Input.GetKeyDown(KeyCode.Space)) // 주사위 돌리기
        {
            randomDiceSides = Random.Range(0, 6);

            rend.sprite = diceSides[randomDiceSides];

            yield return new WaitForSeconds(0.05f);
        }



        finalSide = randomDiceSides + 1;

        Debug.Log(finalSide);
    }
}

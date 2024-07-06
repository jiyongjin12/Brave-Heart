using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvText : MonoBehaviour
{
    public Text renderer;
    Color ColorAlpha;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Text>();
        ColorAlpha = renderer.color;
        ColorAlpha.a = 1;
    }

    public void Fade()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float f = 1;
        while (f > 0)
        {
            f -= 0.1f;
            Color ColorAlhpa = renderer.color;
            ColorAlhpa.a = f;
            renderer.color = ColorAlhpa;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
        ColorAlpha.a = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    //public TMP_Text textMesh;
    //Mesh mesh;
    //Vector3[] vertices;

    //private void Start()
    //{
    //    textMesh = GetComponent<TMP_Text>();
    //}

    //private void Update()
    //{
    //    textMesh.ForceMeshUpdate();
    //    mesh = textMesh.mesh;
    //    vertices = mesh.vertices;

    //    for (int i = 0; i < vertices.Length; i++)
    //    {
    //        Vector3 offset = Wobble(Time.time + i);

    //        vertices[i] = vertices[i] + offset;
    //    }
    //    mesh.vertices = vertices;
    //    textMesh.canvasRenderer.SetMesh(mesh);

    //}

    //Vector2 Wobble(float time)
    //{
    //    return new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * .8f));
    //}

    public TMP_Text textMesh;
    Mesh mesh;
    Vector3[] vertices;

    public Gradient rainbow;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            WobbleText();
        }
        else
        {
            ResetTextWaveMent();
        }

    }

    public void WobbleText()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;
        Color[] colors = mesh.colors;

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            colors[index] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index].x * 0.01f, 1f));
            colors[index + 1] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 1].x * 0.01f, 1f));
            colors[index + 2] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 2].x * 0.01f, 1f));
            colors[index + 3] = rainbow.Evaluate(Mathf.Repeat(Time.time + vertices[index + 3].x * 0.01f, 1f));


            Vector3 offset = Wobble(Time.time + i);
            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * .8f), Mathf.Cos(time * 1f));
    }

    public void ResetTextWaveMent()
    {
        // 텍스트의 위치를 초기 상태로 되돌림
        textMesh.ForceMeshUpdate();
        var textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            var vertices = meshInfo.vertices;

            for (int j = 0; j < vertices.Length; j++)
            {
                vertices[j] = meshInfo.mesh.vertices[j];
            }

            meshInfo.mesh.vertices = vertices;
            textMesh.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

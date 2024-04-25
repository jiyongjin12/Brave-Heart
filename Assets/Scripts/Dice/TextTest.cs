using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTest : MonoBehaviour
{
    public TMP_Text textComponent;
    public float UpDown = 3;
    public float Wave = 0.04f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TextWaveMent();
        }

        if (Input.GetKeyUp(KeyCode.Z)) // 'Z' 키를 뗄 때
        {
            ResetTextWaveMent(); // 텍스트 파동 효과를 초기화하는 함수 호출
        }
    }

    public void TextWaveMent()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * Wave) * UpDown, 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }

    public void ResetTextWaveMent()
    {
        // 텍스트의 위치를 초기 상태로 되돌림
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            var vertices = meshInfo.vertices;

            for (int j = 0; j < vertices.Length; j++)
            {
                vertices[j] = meshInfo.mesh.vertices[j];
            }

            meshInfo.mesh.vertices = vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}

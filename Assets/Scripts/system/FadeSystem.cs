using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FadeSystem : MonoBehaviour
{
    public static FadeSystem Instance
    {
        get
        {
            return Instance;
        }
    }
    public static FadeSystem instance;

    public CanvasGroup Fade_img;
    float fadeDuration = 2; //암전되는 시간

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration).OnComplete(() => { Fade_img.blocksRaycasts = false; });
    }

    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration).OnStart(() =>
        {
            Fade_img.blocksRaycasts = true;
        }).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void ChangeIntScene(int sceneNum)
    {
        Fade_img.DOFade(1, fadeDuration).OnStart(() =>
        {
            Fade_img.blocksRaycasts = true;
        }).OnComplete(() =>
        {
            SceneManager.LoadScene(sceneNum);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider audio;

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void AudioControl()
    {
        float sound = audio.value;

        if (sound == -40f) mixer.SetFloat("BGM", -80);
        else mixer.SetFloat("BGM", sound);
    }
    public void AudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}

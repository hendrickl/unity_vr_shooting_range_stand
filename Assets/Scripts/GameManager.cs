using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioLoadScene;
    [SerializeField] private AudioClip _audioExitGame;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void InvokeLoadScene(int index)
    {
        PlayAudioClip(_audioLoadScene);
        StartCoroutine(WaitAndLoadScene_Coroutine(index, 0.25f));
    }

    private IEnumerator WaitAndLoadScene_Coroutine(int index, float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene(index);
    }

    public void ExitGame()
    {
        PlayAudioClip(_audioExitGame);
        Application.Quit();
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }

    public void StopAudio()
    {
        if (!_audioSource)
        {
            Debug.LogWarning("Audio source is not defined");
            throw new UnityException();
        }

        print("Stop audio");
        _audioSource.Stop();
        // _audioSource.volume = 0f;
    }
}
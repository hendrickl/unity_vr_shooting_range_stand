using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioLoadScene;
    [SerializeField] private AudioSource _audioExitGame;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void InvokeLoadScene(int index)
    {
        _audioLoadScene.Play();
        StartCoroutine(WaitAndLoadScene_Coroutine(index, 1f));
    }

    private IEnumerator WaitAndLoadScene_Coroutine(int index, float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene(index);
    }

    public void ExitGame()
    {
        _audioExitGame.volume = 1f;
        _audioExitGame.Play();
        Application.Quit();
        Debug.Log("Exit pressed");
    }
}
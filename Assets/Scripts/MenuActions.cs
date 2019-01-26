using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public static int PlayerCount { get; private set; }

    public void StartGame(int playerCount)
    {
        PlayerCount = playerCount;
        StartCoroutine(LoadScene("Game"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
        while (!load.isDone)
        {
            yield return null;
        }
    }
}

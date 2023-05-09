using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOperator : MonoBehaviour
{
    public Button ExitGameButton;
    public Button enterMazeButton;
    private string currentSceneName;

    //conect buttons to function
    private void Start()
    {
        ExitGameButton.onClick.AddListener(QuitGame);
        enterMazeButton.onClick.AddListener(PlayGame);
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // change to level 1 scene with i delay of 1,8s
    public void PlayGame()
    {
        StartCoroutine(DelayedSceneSwitch("Level 1",1.8f));
        Debug.Log("sceneSwitch!");
    }
    // exit the game
    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
        Debug.Log("quit!");
    }
    // makes sure that the game object this is attch to dont get destroyd when changing scene
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // declers how the loadscene work. we have a delay of 1.8s
    private IEnumerator DelayedSceneSwitch(string sceneName,float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    // makes the script running while the scene is loading
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}

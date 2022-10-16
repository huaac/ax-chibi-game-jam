using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LoadNextScene : MonoBehaviour
{
    public string nextSceneName;
    public int seconds;
    private string currScene;

    // void OnEnable()
    // {
    //     // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
    //     SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    // }
    void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(sceneName, 115);
        if(currScene != "Menu Screen")
        {
            Invoke("LoadScene", seconds);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // public void LoadScene(string scene_name)
    // {
    //     SceneManager.LoadScene(scene_name);
    // }

    public void QuitGame()
    {
        Application.Quit();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class LoadNextScene : MonoBehaviour
{
    public string nextSceneName;
    public int seconds;
    private string currScene;
    [SerializeField] private Image screen;

    // void OnEnable()
    // {
    //     // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
    //     SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    // }
    void Start()
    {
        currScene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(sceneName, 115);
        if(currScene != "Menu Screen" && currScene != "EndingScene2")
        {
            Invoke("LoadScene", seconds);
        }
        if (currScene == "BossScene")
        {
            Invoke("FadeIn", 163f);
        }
        Debug.Log("NCN: " + nextSceneName);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(currScene);
    }


    private void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        screen.color = Color.Lerp(screen.color, Color.black, 1.5f * Time.deltaTime);
    }

    public IEnumerator FadeInRoutine()
    {
        // Make sure the RawImage is enabled.
        screen.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (screen.color.a >= 0.95f)
            {
                // ... reload the level
                // SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void FadeIn()
    {
        StartCoroutine("FadeInRoutine");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
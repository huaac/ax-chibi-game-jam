using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMusic : MonoBehaviour
{
    private AudioSource audioSrc;
    private string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        scene_name = SceneManager.GetActiveScene().name;
        // audioSrc = GetComponent<AudioSource>();
        // BGSoundScript.Instance.gameObject.GetComponent<AudioSource>() = audioSrc;
        // BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
        
        if (scene_name == "OpeningScene")
        {
            BGSoundScript.PlayOpeningMusic();
        }
        else if(scene_name == "BossScene")
        {
            BGSoundScript.PlayBossMusic();
        }

    }

}

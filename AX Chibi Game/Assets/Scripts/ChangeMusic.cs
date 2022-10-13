using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    private AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        // audioSrc = GetComponent<AudioSource>();
        // BGSoundScript.Instance.gameObject.GetComponent<AudioSource>() = audioSrc;
        // BGSoundScript.Instance.gameObject.GetComponent<AudioSource>().Pause();
        BGSoundScript.PlayBossMusic();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

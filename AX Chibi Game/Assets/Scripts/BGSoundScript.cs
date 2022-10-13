using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundScript : MonoBehaviour
{

    [SerializeField]
    private AudioClip openingMusic;

    [SerializeField]
    private AudioClip bossMusic;

    [SerializeField]
    /// <summary>
    /// The component that plays the music
    /// </summary>
    private AudioSource source;

    /// <summary>
    /// This class follows the singleton pattern and this is its instance
    /// </summary>
    static private BGSoundScript instance;

    /// <summary>
    /// Awake is not public because other scripts have no reason to call it directly,
    /// only the Unity runtime does (and it can call protected and private methods).
    /// It is protected virtual so that possible subclasses may perform more specific
    /// tasks in their own Awake and still call this base method (It's like constructors
    /// in object-oriented languages but compatible with Unity's component-based stuff.
    /// </summary>
    protected virtual void Awake() 
    {
        // Singleton enforcement
        if (instance == null) 
        {
            // Register as singleton if first
            instance = this;
            DontDestroyOnLoad(this);
        } 
        else {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
     }

    protected virtual void Start() 
    {
        // If the game starts in a menu scene, play the appropriate music
        PlayOpeningMusic();
    }

    /// <summary>
     /// Plays the music designed for the menus
     /// This method is static so that it can be called from anywhere in the code.
     /// </summary>
     static public void PlayOpeningMusic ()
     {
         if (instance != null) {
             if (instance.source != null) {
                 instance.source.Stop();
                 instance.source.clip = instance.openingMusic;
                 instance.source.Play();
             }
         } else {
             Debug.LogError("Unavailable MusicPlayer component");
         }
     }
     
    /// <summary>
    /// Plays the music designed for outside menus
    /// This method is static so that it can be called from anywhere in the code.
    /// </summary>
    static public void PlayBossMusic ()
    {
        if (instance != null) {
            if (instance.source != null) {
                instance.source.Stop();
                instance.source.clip = instance.bossMusic;
                instance.source.Play();
            }
         } 
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

}

// void Start()
    // {
        
    // }
    // private static BGSoundScript instance = null;
    // public static BGSoundScript Instance
    // {
    //     get { return instance; }
    // }

    // void Awake()
    // {
    //     if (instance != null && instance != this)
    //     {
    //         Destroy(this.gameObject);
    //         return;
    //     }
    //     else
    //     {
    //         instance = this;
    //     }
    //     DontDestroyOnLoad(this.gameObject);
    // }
    // private static BGSoundScript instance;

    // void Awake()
    // {
    //   if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(instance);

    //     }    

    //   else
    //     {
    //         Destroy(gameObject);
    //     }
       
    // }
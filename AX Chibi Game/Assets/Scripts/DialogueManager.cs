using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> quips;
    [SerializeField] private List<bool> quips_read;
    private int startIndex;


    void Start()
    {
        startIndex = 0;
        for (int x = 0; x < quips.Count; x++)
        {
            quips_read.Add(false);
        }
    }

    void FixedUpdate()
    {
        switch ((int) Mathf.Floor(Time.timeSinceLevelLoad))
        {
            case 30:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 60:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 80:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 85:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 90:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 120:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
            case 150:
                if(quips_read[startIndex] == false)
                {
                    quips[startIndex].active = true;
                    quips_read[startIndex] = true;
                    Invoke("TextDuration",3f);
                }
                break;
        }
    }

    public void defeated()
    {
        quips[7].active = true;
    }

    // private IEnumerator TextDuration()
    // {
    //     yield return new WaitForSeconds(3f);
    //     quips[startIndex].active = false;
    //     // startIndex++;
    // }

    private void TextDuration()
    {
        quips[startIndex].active = false;
        startIndex++;
    }

}

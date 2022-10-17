using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> quips;
    private int startIndex;

    void Start()
    {
        startIndex = 0;
    }

    void FixedUpdate()
    {
        switch (Time.timeSinceLevelLoad)
        {
            case 30:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 60:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 80:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 85:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 90:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 120:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
            case 150:
                quips[startIndex].active = true;
                startIndex++;
                StartCoroutine(TextDuration());
                break;
        }

    }

    private IEnumerator TextDuration()
    {
        yield return new WaitForSeconds(3f);
        quips[startIndex-1].active = false;
    }
}

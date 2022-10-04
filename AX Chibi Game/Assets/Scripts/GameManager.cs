using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float nextAttackTime;
    private float randomMinionTime;
    private float randomTimeHorde;
    public GameObject enemyprefab;
    public List<GameObject> spawnLocations;
    // Start is called before the first frame update
    void Start()
    {
        nextAttackTime = 2f;
        randomMinionTime = 1.5f;
        randomTimeHorde = 5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Randomly decides the next attack and has a cooldown of 2 seconds
        if (Time.time > nextAttackTime)
        {
            int randNum = Random.Range(0, 6);
            switch (randNum)
            {
                case 0:
                    Stab();
                    break;
                case 1:
                    Slash();
                    break;
                case 2:
                    Slash();
                    break;
                case 3:
                    Stab();
                    break;
                case 4:
                    Slash();
                    break;
                case 5:
                    Fireball();
                    break;

            }
            nextAttackTime += 2f;
        }
        //random cooldown and spawns minion
        if (Time.time > randomMinionTime)
        {
            int randLocation = Random.Range(0, 2);
            DelayedMinions(randLocation);
            float randFloat = Random.Range(1.5f, 4f);
            randomMinionTime += randFloat;
        }
        if (Time.time > randomTimeHorde)
        {
            int randLocation = Random.Range(2, 4);
            FullForceHorde(randLocation);
            float increaseRandomTime = Random.Range(3.5f, 8f);
            randomTimeHorde += increaseRandomTime;
        }
    }

    void DelayedMinions(int spawnLocation)
    {
        GameObject enemy = (GameObject)Instantiate(enemyprefab, spawnLocations[spawnLocation].transform.position, spawnLocations[spawnLocation].transform.rotation);
    }

    void FullForceHorde(int spawnLocation)
    {
        StartCoroutine(SpawnEnemyInHorde(spawnLocation));
    }

    private IEnumerator SpawnEnemyInHorde(int spawnLoc)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject enemy = (GameObject)Instantiate(enemyprefab, spawnLocations[spawnLoc].transform.position, spawnLocations[spawnLoc].transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Fireball()
    {
        print("2");
    }

    void Slash()
    {
        print("3");
    }

    void Stab()
    {
        print("4");
    }
}

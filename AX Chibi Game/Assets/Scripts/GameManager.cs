using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float nextAttackTime;
    private float randomMinionTime;
    private float randomTimeHorde;
    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private GameObject stabPrefab;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private List<GameObject> minionSpawns;
    [SerializeField] private List<GameObject> anchors;

    [SerializeField] private GameObject attackSpawnPrefab;
    private GameObject slashSpawnObject;

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
            int randNum = Random.Range(0, 8);
            switch (randNum)
            {
                case 0:
                    Stab();
                    break;
                case 1:
                    Slash(Random.Range(0, 2));
                    break;
                case 2:
                    Slash(Random.Range(0, 2));
                    break;
                case 3:
                    Stab();
                    break;
                case 4:
                    Slash(Random.Range(0, 2));
                    break;
                case 5:
                    Fireball();
                    break;
                case 6:
                    Stab();
                    break;
                case 7:
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
        GameObject enemy = (GameObject)Instantiate(enemyprefab, minionSpawns[spawnLocation].transform.position, minionSpawns[spawnLocation].transform.rotation);
    }

    void FullForceHorde(int spawnLocation)
    {
        StartCoroutine(SpawnEnemyInHorde(spawnLocation));
    }

    private IEnumerator SpawnEnemyInHorde(int spawnLoc)
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject enemy = (GameObject)Instantiate(enemyprefab, minionSpawns[spawnLoc].transform.position, minionSpawns[spawnLoc].transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Fireball()
    {
        GameObject attackSpawnObject = (GameObject)Instantiate(attackSpawnPrefab);
        Transform fireSpawn = attackSpawnObject.transform;

        float spawnTX = Random.Range(anchors[0].transform.position.x, anchors[2].transform.position.x);
        fireSpawn.position = new Vector3(4.25f, spawnTX, 0f);
        GameObject fireObject = (GameObject)Instantiate(fireBallPrefab, fireSpawn);
    }

    void Slash(int leftOrRight)
    {
        GameObject attackSpawnObject = (GameObject)Instantiate(attackSpawnPrefab);
        Transform slashSpawn = attackSpawnObject.transform;
        switch (leftOrRight)
        {
            case 0:
                float spawnLX = anchors[0].transform.position.x;
                float spawnLY = Random.Range(anchors[1].transform.position.y, anchors[0].transform.position.y);
                slashSpawn.position = new Vector3(spawnLX, spawnLY, 0.0f);
                GameObject slashObjectL = (GameObject)Instantiate(slashPrefab, slashSpawn);
                break;
            case 1:
                float spawnRX = anchors[2].transform.position.x;
                float spawnRY = Random.Range(anchors[3].transform.position.y, anchors[2].transform.position.y);
                slashSpawn.position = new Vector3(spawnRX, spawnRY, 0.0f);
                GameObject slashObjectR = (GameObject)Instantiate(slashPrefab, slashSpawn);
                break;
        }
    }

    void Stab()
    {
        GameObject attackSpawnObject = (GameObject)Instantiate(attackSpawnPrefab);
        Transform stabSpawn = attackSpawnObject.transform;

        float spawnX = Random.Range(anchors[0].transform.position.x, anchors[2].transform.position.x);
        float spawnY = Random.Range(anchors[1].transform.position.y, anchors[0].transform.position.y);
        stabSpawn.position = new Vector3(spawnX, spawnY, 0.0f);
        GameObject stabObject = (GameObject)Instantiate(stabPrefab, stabSpawn);
    }
}

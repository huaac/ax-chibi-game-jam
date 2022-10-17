using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float nextAttackTime;
    private float randomMinionTime;
    private float randomTimeHorde;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private GameObject stabPrefab;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private List<GameObject> minionSpawns;
    [SerializeField] private List<GameObject> anchors;


    [SerializeField] private GameObject attackSpawnPrefab;
    [SerializeField] private GameObject Exclamation;
    private GameObject slashSpawnObject;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.active = false;
        nextAttackTime = 2f;
        randomMinionTime = 1.5f;
        randomTimeHorde = 5f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        int playerAlive = player.GetComponent<PlayerHealth>().OutOfHealth();
        switch(playerAlive)
        {
            case 0:
                gameOverText.active = true;
                StartCoroutine(GameOverDelay());
                break;
            default:
                gameOverText.active = false;
                //Randomly decides the next attack and has a cooldown of 2 seconds
                if(Time.timeSinceLevelLoad <= 166)
                {
                    if (Time.timeSinceLevelLoad > nextAttackTime)
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
                        nextAttackTime += 1.8f;
                    }
                    //random cooldown and spawns minion
                    if (Time.timeSinceLevelLoad > randomMinionTime)
                    {
                        int randLocation = Random.Range(0, 2);
                        DelayedMinions(randLocation);
                        float randFloat = Random.Range(1.5f, 4f);
                        randomMinionTime += randFloat;
                    }
                    if (Time.timeSinceLevelLoad > randomTimeHorde)
                    {
                        int randLocation = Random.Range(2, 4);
                        FullForceHorde(randLocation);
                        float increaseRandomTime = Random.Range(3.5f, 8f);
                        randomTimeHorde += increaseRandomTime;
                    }
                    
                }
                else
                {
                    
                    break;
                }
                break;
        }
        
    }

    void DelayedMinions(int spawnLocation)
    {
        StartCoroutine(RedWarning(minionSpawns[spawnLocation].transform));
        StartCoroutine(DelayedSpawn(enemyprefab, minionSpawns[spawnLocation].transform, minionSpawns[spawnLocation].transform));
        //GameObject enemy = (GameObject)Instantiate(enemyprefab, minionSpawns[spawnLocation].transform.position, minionSpawns[spawnLocation].transform.rotation);
    }

    void FullForceHorde(int spawnLocation)
    {
        StartCoroutine(RedWarning(minionSpawns[spawnLocation].transform));
        StartCoroutine(SpawnEnemyInHorde(spawnLocation));
    }

    private IEnumerator SpawnEnemyInHorde(int spawnLoc)
    {
        yield return new WaitForSeconds(1f);
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
        fireSpawn.position = new Vector3(spawnTX, 4.5f, 0f);
        StartCoroutine(RedWarning(fireSpawn));
        StartCoroutine(DelayedSpawn(fireBallPrefab, fireSpawn, fireSpawn));
        StartCoroutine(DestroyPlaceholder(attackSpawnObject));
        //GameObject fireObject = (GameObject)Instantiate(fireBallPrefab, fireSpawn);
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
                StartCoroutine(RedWarning(slashSpawn));
                StartCoroutine(DelayedSpawn(slashPrefab, slashSpawn, slashSpawn));
                //GameObject slashObjectL = (GameObject)Instantiate(slashPrefab, slashSpawn);
                break;
            case 1:
                float spawnRX = anchors[2].transform.position.x;
                float spawnRY = Random.Range(anchors[3].transform.position.y, anchors[2].transform.position.y);
                slashSpawn.position = new Vector3(spawnRX, spawnRY, 0.0f);
                StartCoroutine(RedWarning(slashSpawn));
                StartCoroutine(DelayedSpawn(slashPrefab, slashSpawn, slashSpawn));
                //GameObject slashObjectR = (GameObject)Instantiate(slashPrefab, slashSpawn);
                break;
            
        }
        StartCoroutine(DestroyPlaceholder(attackSpawnObject));
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

    private IEnumerator RedWarning(Transform spawnLocation)
    {
        GameObject exclaim = (GameObject)Instantiate(Exclamation, spawnLocation);
        yield return new WaitForSeconds(1f);
        Destroy(exclaim);
    }

    private IEnumerator DelayedSpawn(GameObject prefab, Transform spawnLocation, Transform spawnRotation)
    {
        yield return new WaitForSeconds(1f);
        GameObject attackObject = (GameObject)Instantiate(prefab, spawnLocation.position, spawnRotation.rotation);
    }
    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(5f);
        player.GetComponent<PlayerHealth>().enabled = false;
        player.GetComponent<PlayerHealth>().enabled = true;
    }

    private IEnumerator DestroyPlaceholder(GameObject placeholder)
    {
        yield return new WaitForSeconds(1f);
        Destroy(placeholder);
    }
}

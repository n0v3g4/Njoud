using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnDelay;
    public int spawns;
    private bool spawnOnDelay = false;
    private int spawnDistance = 1;
    public GameObject[] enemyPrefabs;

    public void InitialiseSpawner()
    {

    }

    void Update()
    {
        if (!spawnOnDelay)
        {
            spawnOnDelay = true;
            StartCoroutine(spawnCooldownReset());
            spawnEnemy(enemyPrefabs[(int)(Random.Range(0, 100)) % enemyPrefabs.Length]);
        }
    }

    private void spawnEnemy(GameObject enemyPrefab)
    {
        //create a monster
        Vector3 spawnLocation = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0f);
        spawnLocation = transform.position + (spawnLocation.normalized * spawnDistance);
        GameObject newItemGo = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        spawns--;
        if (spawns <= 0) Destroy(gameObject);
    }

    private IEnumerator spawnCooldownReset()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnOnDelay = false;
    }
}

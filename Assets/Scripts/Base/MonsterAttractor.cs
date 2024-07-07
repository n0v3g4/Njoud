using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttractor : MonoBehaviour
{
    public int spawnDelay;
    private bool spawnOnDelay = false;
    private int spawnDistance = 5;
    public GameObject[] spawnerPrefabs;

    // Update is called once per frame
    void Update()
    {
        if (!spawnOnDelay)
        {
            spawnOnDelay = true;
            StartCoroutine(spawnCooldownReset());
            spawnSpawner(spawnerPrefabs[(int)(Random.Range(0, 100)) % spawnerPrefabs.Length]);
        }
    }

    private void spawnSpawner(GameObject spawnerPrefab)
    {
        //create a monster spawner
        Vector3 spawnLocation = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0f);
        spawnLocation = transform.position + (spawnLocation.normalized * spawnDistance);
        GameObject newItemGo = Instantiate(spawnerPrefab, spawnLocation, Quaternion.identity);
        newItemGo.GetComponent<EnemySpawner>().InitialiseSpawner();
    }

    private IEnumerator spawnCooldownReset()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnOnDelay = false;
    }
}

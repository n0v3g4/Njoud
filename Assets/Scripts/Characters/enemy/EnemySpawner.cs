using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnDelay;
    public int spawns;
    public Animator animator;
    private bool spawnOnDelay = true;
    public GameObject[] enemyPrefabs;

    public void InitialiseSpawner()
    {
        StartCoroutine(spawnCooldownReset());
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
        Vector3 spawnLocation = transform.position;
        GameObject newItemGo = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        spawns--;
        //this also destroys the spawner once the animation is done
        if (spawns <= 0) animator.SetTrigger("Die");
    }

    private IEnumerator spawnCooldownReset()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnOnDelay = false;
    }
}

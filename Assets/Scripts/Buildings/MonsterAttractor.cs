using UnityEngine;
using System.Collections;

public class MonsterAttractor : MonoBehaviour
{
    public int spawnDelay;
    private int spawnDistance = 5;
    private bool spawnOnDelay = false;
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
        Vector3 spawnLocation = (Random.insideUnitCircle.normalized * spawnDistance);
        spawnLocation += transform.position;
        GameObject newItemGo = Instantiate(spawnerPrefab, spawnLocation, Quaternion.identity);
        newItemGo.GetComponent<EnemySpawner>().InitialiseSpawner();
    }

    private IEnumerator spawnCooldownReset()
    {
        yield return new WaitForSeconds(spawnDelay);
        spawnOnDelay = false;
    }

    //display range in editor
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnDistance);
    }
}

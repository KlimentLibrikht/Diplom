using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobs : MonoBehaviour
{
    public GameObject MobToSpawn;
    public int numberOfMobsAtOnce;
    public int spawnRange;
    public float timeToWaitBeforeRespawn;
    public List<GameObject> mobsSpawned;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numberOfMobsAtOnce; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                transform.position.y,
                Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

            GameObject mobSpawned = Instantiate(MobToSpawn, randomPosition, Quaternion.identity) as GameObject;
            mobSpawned.transform.parent = this.transform;
            mobsSpawned.Add(mobSpawned);
        }
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {

        for (int i = 0; i < mobsSpawned.Count; i++)
        {
            if (mobsSpawned[i] == null)
            {
                mobsSpawned.RemoveAt(i);
            }
        }

        if (mobsSpawned.Count < numberOfMobsAtOnce)
        {

            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                transform.position.y,
                Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

            GameObject mobSpawned = Instantiate(MobToSpawn, randomPosition, Quaternion.identity) as GameObject;
            mobSpawned.transform.parent = this.transform;
            mobsSpawned.Add(mobSpawned);
        }



        yield return new WaitForSeconds(timeToWaitBeforeRespawn);
        StartCoroutine(SpawnEnemy());

    }
}

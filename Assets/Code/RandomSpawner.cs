using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public static RandomSpawner instance = null;

    public GameObject[] enemy;
    public GameObject[] point;

    // рандом точки
    int randPoint;
    int randEnemy;

    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;

    int enemiesOnScreen = 0;

    public float spawnDelay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}

        DontDestroyOnLoad(gameObject);
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                randPoint = UnityEngine.Random.Range(0, point.Length);
                randEnemy = UnityEngine.Random.Range(0, enemy.Length);
                if (enemiesOnScreen < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemy[randEnemy]);
                    newEnemy.transform.position = point[randPoint].transform.position;
                    newEnemy.gameObject.SetActive(true);
                    enemiesOnScreen += 1;
                    yield return new WaitForSeconds(spawnDelay);
                }
            }
        }
    }
}

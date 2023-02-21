using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int ENEMY_COUNT = 17;
    
    private int waveNumber = 1;

    private float remainingTime = 10;

    [SerializeField] private float[] corners;

    [SerializeField] private GameObject[] enemyPrefabs = new GameObject[ENEMY_COUNT];
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        //remainingTime -= Time.deltaTime;
    }

    IEnumerator SpawnEnemy()
    {
        while (remainingTime > 0)
        {
            yield return new WaitForSeconds(Random.Range(0.8f, 2.5f));

            int randomIndex = Random.Range(0, ENEMY_COUNT);

            float xDiff = corners[0] - corners[1];
            float yDiff = corners[2] - corners[3];

            float randomX = Random.Range(0, xDiff);
            float randomY = Random.Range(0, yDiff);

            Vector3 spawnPos = new Vector3(corners[1] + randomX, corners[3] + randomY, 0.10f);

            Instantiate(enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
        }
    }
}

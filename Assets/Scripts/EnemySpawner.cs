using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject slimeEnemyPrefab;
    public List<Transform> spawnLocations; // Assign these in the Inspector


    public float killPercentage = 0.8f; // Percentage of slimes to be killed
    private bool hasKilled = false;

    private void Awake()
    {
        SpawnSlimeEnemies();
    }


    void SpawnSlimeEnemies()
    {
        foreach (Transform spawnLocation in spawnLocations)
        {
            int numToSpawn = Random.Range(1, 6); // Random number between 1 and 5

            for (int i = 0; i < numToSpawn; i++)
            {
                Instantiate(slimeEnemyPrefab, spawnLocation.position, Quaternion.identity);
            }
        }
    }

    void KillSlimes()
    {
        List<GameObject> slimes = new List<GameObject>();

        // Find all slime enemies in the scene
        GameObject[] slimeEnemies = GameObject.FindGameObjectsWithTag("SlimeEnemy");
        slimes.AddRange(slimeEnemies);

        // Determine the number of slimes to kill
        int slimesToKill = Mathf.CeilToInt(slimes.Count * killPercentage);

        // Shuffle the list of slimes
        for (int i = 0; i < slimes.Count; i++)
        {
            GameObject temp = slimes[i];
            int randomIndex = Random.Range(i, slimes.Count);
            slimes[i] = slimes[randomIndex];
            slimes[randomIndex] = temp;
        }

        // Kill the required number of slimes
        for (int i = 0; i < slimesToKill; i++)
        {
            Destroy(slimes[i]);
        }
    }
}

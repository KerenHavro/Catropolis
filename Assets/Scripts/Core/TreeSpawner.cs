using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    public GameObject treePrefab;
    public List<Transform> spawnLocations; // Assign these in the Inspector

    public float spawnInterval = 5f; // Time between tree spawns
    private float timer = 0f;
    private void Start()
    {
        SpawnTrees();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnTrees();
            timer = 0f;
        }
    }

    void SpawnTrees()
    {
        foreach (Transform spawnLocation in spawnLocations)
        {
            if (!HasTreeAtLocation(spawnLocation.position))
            {
                GameObject tree= Instantiate(treePrefab, spawnLocation.position, Quaternion.identity);
                tree.gameObject.name = "Tree";
            }
        }
    }

    bool HasTreeAtLocation(Vector3 location)
    {
        Collider[] colliders = Physics.OverlapSphere(location, 1f); // Adjust the radius as needed
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Object" +
                ""))
            {
                return true;
            }
        }
        return false;
    }
}
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("Virus Prefab")]
    public GameObject virusPrefab;

    [Header("Spawn Effect Prefab (Optional)")]
    public GameObject spawnEffectPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int virusesPerWave = 3;
    public float timeBetweenWaves = 10f;

    [Header("Max Active Viruses")]
    public int maxViruses = 10;

    private List<GameObject> activeViruses = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(2f); // initial delay

        while (true)
        {
            int spawnedThisWave = 0;

            while (spawnedThisWave < virusesPerWave && activeViruses.Count < maxViruses)
            {
                SpawnVirus();
                spawnedThisWave++;
                yield return new WaitForSeconds(0.5f); // short delay between individual spawns
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnVirus()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        int index = UnityEngine.Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];

        GameObject virus = Instantiate(virusPrefab, spawnPoint.position, Quaternion.identity);

        // Add to active list
        activeViruses.Add(virus);

        // Clean up when virus is destroyed
        VirusController virusScript = virus.GetComponent<VirusController>();
        if (virusScript != null)
        {
            virusScript.OnDestroyed += () => { activeViruses.Remove(virus); };
        }

        // Spawn visual effect
        if (spawnEffectPrefab != null)
        {
            Instantiate(spawnEffectPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
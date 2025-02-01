using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class WaveSettings
    {
        public GameObject[] MonsterPrefabs;
        public float SpawnDelay;
    }
    [Header("Spawner Settings")]
    [SerializeField] private List<WaveSettings> monsterWaves;
    [SerializeField] private Transform spawnPosition;
    private int _currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        while (_currentWaveIndex < monsterWaves.Count)
        {
            WaveSettings currentWave = monsterWaves[_currentWaveIndex];

            foreach (GameObject monsterPrefab in currentWave.MonsterPrefabs)
            {
                Instantiate(monsterPrefab, spawnPosition.position, Quaternion.identity);
                yield return new WaitForSeconds(currentWave.SpawnDelay);
            }

            _currentWaveIndex++; // Move to the next wave
            yield return new WaitForSeconds(5f); // Delay before next wave
        }
    }
}

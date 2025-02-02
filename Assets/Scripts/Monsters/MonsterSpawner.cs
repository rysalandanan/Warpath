using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI waveCount;
    private int _currentWaveIndex = 0;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
        while (_currentWaveIndex < monsterWaves.Count)
        {
            UpdateWaveCountUI();
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
    private void UpdateWaveCountUI()
    {
        waveCount.text = "Wave: " + (_currentWaveIndex + 1);
    }
}

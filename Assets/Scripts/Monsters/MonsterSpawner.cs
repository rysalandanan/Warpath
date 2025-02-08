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
    [SerializeField] private GameObject wavePrompt;

    private int _currentWaveIndex = 0;
    private List<GameObject> activeMonsters = new List<GameObject>();
    private bool isWaveActive = false;

    public void StartWave()
    {
        if (!isWaveActive)
        {
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        isWaveActive = true;
        WavePromptActive(false);
        UpdateWaveCountUI();

        if (_currentWaveIndex >= monsterWaves.Count)
        {
            Debug.Log("All waves completed!");
            yield break;
        }

        WaveSettings currentWave = monsterWaves[_currentWaveIndex];

        foreach (GameObject monsterPrefab in currentWave.MonsterPrefabs)
        {
            GameObject spawnedMonster = Instantiate(monsterPrefab, spawnPosition.position, Quaternion.identity);
            activeMonsters.Add(spawnedMonster);
            yield return new WaitForSeconds(currentWave.SpawnDelay);
        }

        yield return new WaitUntil(() => activeMonsters.Count == 0);

        isWaveActive = false;
        WavePromptActive(true);
        _currentWaveIndex++;
    }

    public void MonsterDefeated(GameObject monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
        }
    }

    private void UpdateWaveCountUI()
    {
        waveCount.text = _currentWaveIndex + 1.ToString();
    }
    private void WavePromptActive(bool yesNo)
    {
        wavePrompt.SetActive(yesNo);
    }
}

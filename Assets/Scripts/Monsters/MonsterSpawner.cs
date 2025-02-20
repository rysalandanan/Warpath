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
    [SerializeField] private GamePhase gamePhase;
    private int _currentWaveIndex = 0;
    private List<GameObject> activeMonsters = new List<GameObject>();

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }
    private IEnumerator SpawnWave()
    {
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
        UpdateGamePhase();
        _currentWaveIndex++;
    }
    private void UpdateGamePhase()
    {
        gamePhase.IsBuildPhase(true);
    }
    public void MonsterDefeated(GameObject monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
        }
    }
}

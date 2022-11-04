using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private int _spawnDelay;
    [SerializeField] private int _cooldownBetweenWaves;
    [SerializeField] private List<Wave> _wave;

    private Wave _currentWave;
    private Coroutine _currentSpawnRoutine;
    private EnemyFactory _enemyFactory;
    private Enemy _currentEnemy;

    private int _currentSpawnedEnemyCount;
    private int _currentKilledEnemies;
    private int _maxEnemyCount;
    private int _currentWaveNumber;

    private void Awake()
    {
        _currentWaveNumber = 0;
        _enemyFactory = GetComponent<EnemyFactory>();
    }

    private void Start() =>
        StartCoroutine(StartCooldownBetweenWaves());

    private void OnDisable() =>
        StopSpawnCoroutine();

    private IEnumerator StartEnemySpawning()
    {
        SetNextWaveValues();
        Enemy currentEnemy = _currentWave.Enemy;
        var waitingTime = new WaitForSeconds(_spawnDelay);

        while (_currentSpawnedEnemyCount != _maxEnemyCount)
        {
            var enemy = _enemyFactory.CreateEnemy(transform, currentEnemy, _playerHealth);
            _currentSpawnedEnemyCount++;
            enemy.WasDying += OnAddDiedEnemyCount;
            yield return waitingTime;
        }

        _currentWaveNumber++;
    }

    private void SetNextWaveValues()
    {
        _currentKilledEnemies = 0;
        _currentSpawnedEnemyCount = 0;

        _currentWave = _wave[_currentWaveNumber];
        _maxEnemyCount = _currentWave.Count;
    }

    private IEnumerator StartCooldownBetweenWaves()
    {
        yield return new WaitForSeconds(_cooldownBetweenWaves);
        _currentSpawnRoutine = StartCoroutine(StartEnemySpawning());
    }

    private void OnAddDiedEnemyCount(Enemy enemy)
    {
        _currentKilledEnemies++;

        if (_currentKilledEnemies >= _maxEnemyCount)
        {
            StopSpawnCoroutine();
            _currentSpawnRoutine = StartCoroutine(StartCooldownBetweenWaves());
        }

        enemy.WasDying -= OnAddDiedEnemyCount;
    }

    private void StopSpawnCoroutine()
    {
        if (_currentSpawnRoutine != null)
        {
            StopCoroutine(_currentSpawnRoutine);
            _currentSpawnRoutine = null;
        }
    }
}
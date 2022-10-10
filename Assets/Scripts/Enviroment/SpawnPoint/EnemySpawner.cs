using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _entity;
    [SerializeField] private float _cooldown;
    [SerializeField] private bool _isWork;
    [SerializeField] private Transform[] _points;

    private Coroutine _spawner;

    public void DisableSpawner()
    {
        if (_spawner != null)
        {
            StopCoroutine(_spawner);
            _spawner = null;
        }
    }

    private void OnEnable()
    {
        int firstElement = 1;
        _points = GetComponentsInChildren<Transform>().Skip(firstElement).ToArray();
        _spawner = StartCoroutine(SpawnEnemies());
    }

    private void OnDisable() => DisableSpawner();

    private IEnumerator SpawnEnemies()
    {
        var waitingTime = new WaitForSeconds(_cooldown);
        bool isWork = true;

        while (isWork)
        {
            for (int i = 0; i < _points.Length; i++)
            {
                if (_isWork == false)
                    yield break;

                Instantiate(_entity, _points[i].position, Quaternion.identity);
                yield return waitingTime;
            }
        }
    }
}

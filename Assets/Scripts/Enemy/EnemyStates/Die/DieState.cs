using UnityEngine;

public class DieState : State
{
    [SerializeField] private EnemyDieBehaviour _enemyDieBehaviour;

    public  void OnEnable()
    {
        _enemyDieBehaviour.enabled = true;
    }
}
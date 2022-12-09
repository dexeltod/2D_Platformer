using UnityEngine;

public class WinState : State
{
    [SerializeField] private EnemyWinBehaviour _enemyWin;

    private void OnEnable()
    {
        _enemyWin.enabled = true;
    }

    private void OnDisable()
    {
        _enemyWin.enabled = false;
    }
}
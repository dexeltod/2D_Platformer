using UnityEngine;

public class MoveToPlayerState : State
{
    [SerializeField] private MoveToPlayerBehaviour _moveToPlayerBehaviour;
    
    private void OnEnable() =>
        _moveToPlayerBehaviour.enabled = true;
    
    private void OnDisable() =>
        _moveToPlayerBehaviour.enabled = false;
}
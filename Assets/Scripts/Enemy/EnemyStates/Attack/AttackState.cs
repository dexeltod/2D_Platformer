using System;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private AttackPlayerBehaviour _attackBehaviour;
    
    private void OnEnable()
    {
        _attackBehaviour.enabled = true;
    }
    
    private void OnDisable()
    {
        _attackBehaviour.enabled = false;
    }
}
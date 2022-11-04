using System;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using PlayerScripts.States;
using UnityEngine;

[RequireComponent(typeof(InputSystemReader), typeof(Animator), typeof(AnimationHasher))]
[RequireComponent(typeof(Player))]

public class PlayerBehaviour : MonoBehaviour, IStateSwitcher
{
    public Action AttackEnded;
    
    private Player _player;
    private AnimationHasher _animationHasher;
    private Animator _animator;
    private WeaponFactory _weaponFactory;

    private BaseState _currentState;
    private List<BaseState> _states = new();

    private void Awake()
    {
        _weaponFactory = GetComponent<WeaponFactory>();
        _player = GetComponent<Player>();
        _animationHasher = GetComponent<AnimationHasher>();
        _animator = GetComponent<Animator>();
        InitializeStates();
    }

    public void SetIdleState()
    {
        SwitchState<PlayerIdleState>();
    }

    public void SetAttackState()
    {
        SwitchState<PlayerAttackState>();
    }

    public void SwitchState<T>() where T : BaseState
    {
        var state = _states.FirstOrDefault(state => state is T);
        _currentState?.Stop();
        state?.Start();
        _currentState = state;
    }

    private void InitializeStates()
    {
        Weapon weapon = _weaponFactory.CreateWeapon(transform, _animator, _animationHasher);

        _states = new List<BaseState>()
        {
            new PlayerIdleState(_player, this, _animationHasher, _animator),
            new PlayerAttackState(_player, this, _animationHasher, _animator, weapon),
        };

        _currentState = _states[0];
    }
}
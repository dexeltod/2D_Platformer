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

    private BaseState _currentState;
    private List<BaseState> _states = new();

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animationHasher = GetComponent<AnimationHasher>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
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
        _states = new List<BaseState>()
        {
            new PlayerIdleState(_player, this, _animationHasher, _animator),
            new PlayerAttackState(_player, this, _animationHasher, _animator, _player.Weapon),
        };

        _currentState = _states[0];
    }
}
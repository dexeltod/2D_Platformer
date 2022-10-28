using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Player _target;
    private State _currentState;
    public State CurrentState => _currentState;

    private void Start()
    {        
        _target = GetComponentInParent<Enemy>().Target;
        _currentState = _firstState;
        _currentState.Enter(_target);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Reset(State startState)
    {
        _currentState = startState;

        if(_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (nextState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
} 

using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _firstState;

    private PlayerEntity _target;
    private EnemyState _currentState;
    public EnemyState CurrentState => _currentState;

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

    private void Reset(EnemyState startState)
    {
        _currentState = startState;

        if(_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(EnemyState nextState)
    {
        if (nextState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
} 

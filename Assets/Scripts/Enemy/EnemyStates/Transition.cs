using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    public State TargetNextState => _nextState;
    public bool IsNeedTransition { get; protected set; }
    protected Player Target { get; private set; }

    public void Initialize(Player target)
    {
        Target = target;
    }

    public abstract void Enable();

    private void OnEnable()
    {
        IsNeedTransition = false;
        Enable();
    }
}

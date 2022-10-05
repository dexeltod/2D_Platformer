using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private EnemyState _enemyState;

    protected PlayerEntity Target { get; private set; }

    public EnemyState TargetState => _enemyState;

    public bool IsNeedTransition { get; protected set; }

    public void Initialize(PlayerEntity target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        IsNeedTransition = false;
        Enable();
    }

    public abstract void Enable();
}

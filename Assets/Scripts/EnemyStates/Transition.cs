using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private EnemyState _enemyState;

    public EnemyState TargetState => _enemyState;
    public bool IsNeedTransition { get; protected set; }
    protected PlayerCharacter Target { get; private set; }

    public void Initialize(PlayerCharacter target)
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

using UnityEngine;
using UnityEngine.Events;

public class EnemyDetectEntity : EnemyEntity
{
    [SerializeField] private UnityEvent _notLooked;

    public event UnityAction NotLooked
    {
        add => _notLooked.AddListener(value);
        remove => _notLooked.RemoveListener(value);
    }

    public override void Start()
    {
        base.Start();
    }
}

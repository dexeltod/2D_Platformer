using UnityEngine;

public class IdleToPatrolTransition : Transition
{
    [SerializeField] private DataIdleState _idleStateData;

    private float _startTime;
    private float _idleTime;

    public override void Enable()
    {
        _startTime = Time.time;
    }

    private void Start()
    {
        SetRandomIdleTime();
    }

    private void Update()
    {
        SetNeedTransition();
    }

    private void SetNeedTransition()
    {
        if (Time.time > _startTime + _idleTime)
        {
            IsNeedTransition = true;
        }
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_idleStateData.MinIdleTime, _idleStateData.MaxIdleTime);
    }    
}

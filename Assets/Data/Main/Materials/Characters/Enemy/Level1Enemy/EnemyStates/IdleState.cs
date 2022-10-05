using UnityEngine;

public class IdleState : State
{
    protected D_IdleState IdleStateData;
    
    
    protected EnemyIdleEntity EnemyIdleEntity;
    protected float IdleTime;

    public IdleState(EntityAnimation entityAnimation, FiniteStateMachine stateMachine, string animBoolName,
        D_IdleState idleStateData, EnemyLookAround enemyLookAround, EnemyIdleEntity enemyIdleEntity)
        : base(entityAnimation, stateMachine, animBoolName)
    {
        this.IdleStateData = idleStateData;
        EntityAnimationAnimation = entityAnimation;
        EnemyIdleEntity = enemyIdleEntity;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
    }

    
}

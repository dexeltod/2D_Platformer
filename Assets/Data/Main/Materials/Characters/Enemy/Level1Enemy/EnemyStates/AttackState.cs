using UnityEngine;

public class AttackState : State
{
    protected PlayerEntity _playerEntity;
    protected D_AttackState _attackStateData;

    public AttackState(EntityAnimation entityAnimation, FiniteStateMachine stateMachine, string animBoolName, D_AttackState attackState)
        : base(entityAnimation, stateMachine, animBoolName)
    {
        _attackStateData = attackState;
    }

    public override void Enter()
    {           
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

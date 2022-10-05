
public class EnemyDetectState : State
{
    protected PlayerEntity PlayerEntity;
    protected EnemyDetectEntity EnemyDetect;
    protected D_EnemyDetectState StateData;

    public EnemyDetectState(EntityAnimation entityAnimation, FiniteStateMachine stateMachine, string animBoolName, D_EnemyDetectState stateData,EnemyDetectEntity enemyDetect)
        : base(entityAnimation, stateMachine, animBoolName)
    {
        EnemyDetect = enemyDetect;
        this.StateData = stateData;
        this.EntityAnimationAnimation = entityAnimation;
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

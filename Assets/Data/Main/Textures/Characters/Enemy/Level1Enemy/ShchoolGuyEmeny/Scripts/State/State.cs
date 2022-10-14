using UnityEngine;
public class State
{
    protected FiniteStateMachine StateMachine;
    protected EntityAnimation EntityAnimationAnimation;

    protected float StartTime;

    protected string AnimBoolName;

    public State(EntityAnimation entityAnimation, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.EntityAnimationAnimation = entityAnimation;
        this.StateMachine = stateMachine;
        this.AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        StartTime = Time.time;
        EntityAnimationAnimation.Anim.SetBool(AnimBoolName, true);
    }

    public virtual void Exit()
    {
        EntityAnimationAnimation.Anim.SetBool(AnimBoolName, false);
    }

    public virtual void LogicUpdate(){}

    public virtual void PhysicsUpdate(){}
}

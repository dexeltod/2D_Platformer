using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public FiniteStateMachine StateMachine;
    

    public AliveGO AliveGO { get; protected set; }
    public Animator Anim { get; protected set; }

    protected EnemyLookAround EnemyLookAround { get; set; }
    protected EnemyPatrolEntity EnemyPatrol { get; set; }
    protected EntityAnimation EntityAnimation { get; set; }
    protected EnemyDetectEntity EnemyDetect { get; set; }
    protected EnemyIdleEntity EnemyIdle { get; set; }

    protected Rigidbody2D Rb2d;
    
    protected Vector2 VelocityWorkspace;
    
    public virtual void Start()
    {
        

        StateMachine = new FiniteStateMachine();
        AliveGO = GetComponentInParent<AliveGO>();
        Anim = GetComponentInParent<Animator>();
        EntityAnimation = GetComponentInChildren<EntityAnimation>();
        EnemyPatrol = GetComponentInChildren<EnemyPatrolEntity>();
        EnemyLookAround = GetComponentInChildren<EnemyLookAround>();
        EnemyIdle = GetComponentInChildren<EnemyIdleEntity>();
        EnemyDetect = GetComponentInChildren<EnemyDetectEntity>();
        Rb2d = GetComponentInParent<Rigidbody2D>();
        
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        if (StateMachine.CurrentState == null || StateMachine == null)
        {
            Debug.Log("Current state is Null Anall");
        }
        else
        {
            Debug.Log("Current state not null");
        }
        Rb2d.velocity = new Vector3(VelocityWorkspace.x, Rb2d.velocity.y);
    }
}

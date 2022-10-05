using UnityEngine;

public class EntityAnimation : EnemyEntity
{
    public override void Start()
    {
        base.Start();
        Anim = AliveGO.gameObject.GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public virtual void Flip()
    {
        AliveGO.transform.Rotate(0f, 180f, 0);
    }
}

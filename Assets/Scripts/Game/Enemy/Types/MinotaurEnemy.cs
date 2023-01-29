using UnityEngine;

namespace Game.Enemy.Types
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class MinotaurEnemy : Enemy
    {
    
    }
}
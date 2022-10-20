using System;
using UnityEngine;

public class EnemyAnimationHashes : MonoBehaviour
{
    public int MoveHash { get; private set; }
    public int AttackHash { get; private set; }
    public int StunHash { get; private set; }

    private void Awake()
    {
        MoveHash = Animator.StringToHash("isMove");
        AttackHash = Animator.StringToHash("isAttack");
        StunHash = Animator.StringToHash("isStunned");
    }
}
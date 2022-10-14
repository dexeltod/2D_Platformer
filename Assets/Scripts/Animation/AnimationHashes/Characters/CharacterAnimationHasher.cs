using UnityEngine;

public class CharacterAnimationHasher : MonoBehaviour
{
    public int AttackSpeedHash { get; private set; }
    public int RacingHash { get; private set; }
    public int AttackHash { get; private set; }
    public int WalkHash { get; private set; }
    public int IdleHash { get; private set; }
    public int RunHash { get; private set; }

    private void Awake()
    {
        WalkHash = Animator.StringToHash("isWalk");
        IdleHash = Animator.StringToHash("isIdle");
        RunHash = Animator.StringToHash("isRunning");
        RacingHash = Animator.StringToHash("isRacing");
        AttackHash = Animator.StringToHash("isAttack");
        AttackSpeedHash = Animator.StringToHash("attackSpeed");
    }
}

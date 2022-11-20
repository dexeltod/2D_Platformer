using UnityEngine;

public class AnimationHasher : MonoBehaviour
{
	public int AttackSpeedHash { get; private set; }
	public int RacingHash { get; private set; }
	public int AttackHash { get; private set; }
	public int WalkHash { get; private set; }
	public int IdleHash { get; private set; }
	public int RunHash { get; private set; }
	public int FunHash { get; private set; }
	public int DieHash { get; private set; }
	public int JumpHash { get; private set; }
	public int FallHash { get; private set; }
	public int GlideHash { get; private set; }

	private void Awake()
	{
		WalkHash = Animator.StringToHash("isWalk");
		IdleHash = Animator.StringToHash("isIdle");
		RunHash = Animator.StringToHash("isRun");
		RacingHash = Animator.StringToHash("isRacing");
		AttackHash = Animator.StringToHash("isAttack");
		AttackSpeedHash = Animator.StringToHash("attackSpeed");
		FunHash = Animator.StringToHash("isFun");
		DieHash = Animator.StringToHash("isDie");
		JumpHash = Animator.StringToHash("isJump");
		FallHash = Animator.StringToHash("isFall");
		GlideHash = Animator.StringToHash("isGlide");
	}
}
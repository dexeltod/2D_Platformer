using System.Collections;
using PlayerScripts.Weapons;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
	[SerializeField] private WeaponInfo _weaponInfo;
	[SerializeField] private bool _isBought = false;

	private Animator _animator;
	private AnimationHasher _animations;

	public bool IsBought => _isBought;
	public bool CanAttack { get; protected set; }
	public int Damage { get; protected set; }
	public float AttackSpeed { get; protected set; }
	public float Range { get; protected set; }

	public abstract IEnumerator AttackRoutine(float direction);
	public abstract void GiveDamage(Enemy target);

	public void Initialize(Animator animator, AnimationHasher hasher)
	{
		_animator = animator;
		_animations = hasher;

		CanAttack = true;
		Damage = _weaponInfo.Damage;
		AttackSpeed = _weaponInfo.AttackSpeed;
		Range = _weaponInfo.Range;
	}

	public void SetBoughtStateTrue() => _isBought = false;

	protected virtual void Awake() => OnAwake();

	protected virtual void OnAwake()
	{
	}

	protected void SetAttackAnimation() =>
		_animator.CrossFade(_animations.AttackHash, 0);
}
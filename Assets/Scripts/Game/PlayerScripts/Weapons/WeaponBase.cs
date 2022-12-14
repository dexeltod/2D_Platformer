using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts.Weapons
{
	public abstract class WeaponBase : MonoBehaviour
	{
		[SerializeField] private ContactFilter2D _enemyFilter;
		[SerializeField] private WeaponInfo _weaponInfo;
		[SerializeField] private bool _isBought = false;
		
		protected MeleeWeaponTriggerInformant MeleeWeaponTriggerInformant;
		protected Animator Animator;
		protected AnimationHasher AnimationHasher;
		protected bool _isGrounded;
		protected bool IsRun;

		public ContactFilter2D EnemyFilter => _enemyFilter;
		public int CurrentAnimationHash { get; protected set; }
		public bool IsBought => _isBought;
		public bool CanAttack { get; protected set; }
		public int Damage { get; protected set; }
		public float AttackSpeed { get; protected set; }
		public float Range { get; protected set; }

		public event Action AttackAnimationEnded;
		
		public abstract IEnumerator AttackRoutine(float direction);
		public abstract void GiveDamage(Enemy target);

		public void Initialize(Animator animator, AnimationHasher hasher, MeleeWeaponTriggerInformant meleeWeaponTrigger)
		{
			MeleeWeaponTriggerInformant = meleeWeaponTrigger;
			Animator = animator;
			AnimationHasher = hasher;

			CanAttack = true;
			Damage = _weaponInfo.Damage;
			AttackSpeed = _weaponInfo.AttackSpeed;
			Range = _weaponInfo.Range;
			
			Animator.SetFloat(AnimationHasher.AttackSpeedHash, AttackSpeed);
		}

		public void SetGroundedBool(bool isGrounded) => _isGrounded = isGrounded;

		public void SetRunBool(Vector2 direction) =>
			IsRun = direction.x != 0 || direction.y != 0;

		public void SetBoughtStateTrue() => _isBought = false;

		protected virtual void Awake() => OnAwake();

		protected virtual void OnAwake()
		{
		}

		protected void OnAnimationEnded() => AttackAnimationEnded.Invoke();

		protected virtual void PlayAttackAnimation(int hash)
		{
		}
	}
}
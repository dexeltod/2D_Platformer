using System;
using Game.Animation.AnimationHashes.Characters;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
	public abstract class AbstractWeapon : Item
	{
		[SerializeField] private ContactFilter2D _enemyFilter;
		[SerializeField] private WeaponInfo _weaponInfo;

		protected MeleeTrigger.MeleeWeaponTriggerInformant MeleeWeaponTriggerInformant;
		protected Animator Animator;
		protected AnimationHasher AnimationHasher;
		protected bool _isGrounded;
		protected bool IsRun;

		public ContactFilter2D EnemyFilter => _enemyFilter;
		public int CurrentAnimationHash { get; protected set; }
		public bool CanAttack { get; protected set; }
		public int Damage { get; protected set; }
		public float AttackSpeed { get; protected set; }
		public float Range { get; protected set; }

		public event Action AttackAnimationEnded;

		protected virtual void Awake() =>
			OnAwake();

		public void Initialize(Animator animator, AnimationHasher hasher,
			MeleeTrigger.MeleeWeaponTriggerInformant meleeWeaponTrigger)
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

		public virtual void Use()
		{
			Attack();
		}

		public abstract void GiveDamage(Enemy.Enemy target);

		public void SetGroundedBool(bool isGrounded) => _isGrounded = isGrounded;

		public void SetRunBool(bool isRun) =>
			IsRun = isRun;

		protected virtual void Attack()
		{
			
		}
		
		protected virtual void OnAwake()
		{
		}

		protected void OnAnimationEnded() => AttackAnimationEnded.Invoke();

		protected virtual void PlayAttackAnimation(int hash)
		{
		}
	}
}
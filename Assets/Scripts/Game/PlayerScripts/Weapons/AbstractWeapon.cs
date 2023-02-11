using System;
using System.Collections;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Weapons.MeleeTrigger;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
	public abstract class AbstractWeapon : Item
	{
		[SerializeField] private ContactFilter2D _enemyFilter;
		[SerializeField] private WeaponInfo _weaponInfo;

		protected MeleeWeaponTriggerInformant MeleeWeaponTriggerInformant;
		protected Animator Animator;
		protected AnimationHasher AnimationHasher;
		protected bool IsRun;
		protected Coroutine AttackRoutine;

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
			MeleeWeaponTriggerInformant meleeWeaponTrigger)
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

		public void Use() => 
			Attack();

		public abstract void GiveDamage(Enemy.Enemy target);


		public void SetRunBool(bool isRun) =>
			IsRun = isRun;

		protected virtual void Attack()
		{
		}

		protected virtual void OnAwake()
		{
		}

		protected IEnumerator PlayAnimationRoutine(int hash)
		{
			Animator.SetBool(hash, true);
			AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);
			float speed = info.speed;
			WaitForSeconds animationTime = new(speed);

			yield return animationTime;
			
			Animator.SetBool(hash, false);
			AttackAnimationEnded.Invoke();
		}

		protected virtual void PlayAttackAnimation(int hash)
		{
		}
	}
}
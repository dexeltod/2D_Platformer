using System;
using System.Collections;
using Game.Animation.AnimationHashes.Characters;
using Game.PlayerScripts.Weapons.MeleeTrigger;
using UnityEngine;

namespace Game.PlayerScripts.Weapons
{
	public abstract class AbstractWeapon : Item
	{
		private const string BaseLayer = "Base Layer";

		[SerializeField] private WeaponInfo _weaponInfo;
		
		protected Animator Animator;
		protected AnimationHasher AnimationHasher;
		protected bool IsRun;
		protected Coroutine AttackRoutine;
		
		private AnimatorFacade _animatorFacade;
		private MeleeWeaponTrigger _meleeWeaponTrigger;
		public int CurrentAnimationHash { get; protected set; }
		public bool CanAttack { get; protected set; }
		public int Damage { get; protected set; }
		public float AttackSpeed { get; protected set; }
		public float Range { get; protected set; }

		public event Action AttackAnimationEnded;

		protected virtual void Awake()
		{
			OnAwake();
		}

		private void OnEnable() => 
			_meleeWeaponTrigger.Touched += GiveDamage;

		private void OnDisable() => 
			_meleeWeaponTrigger.Touched -= GiveDamage;

		public void Initialize(Animator animator, AnimatorFacade animatorFacade, AnimationHasher hasher,
			MeleeWeaponTrigger meleeWeaponTrigger)
		{
			_meleeWeaponTrigger = meleeWeaponTrigger;
			_animatorFacade = animatorFacade;
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
			_meleeWeaponTrigger.gameObject.SetActive(true);
			_animatorFacade.Play(hash);
			AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(Animator.GetLayerIndex(BaseLayer));
			float speed = info.length;
			WaitForSeconds animationTime = new(speed);

			yield return animationTime;
			
			_meleeWeaponTrigger.gameObject.SetActive(false);
			AttackAnimationEnded.Invoke();
		}
	}
}
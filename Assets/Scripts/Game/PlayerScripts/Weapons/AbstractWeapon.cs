using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Game.Animation.AnimationHashes.Characters;
using Game.Enemy;
using Game.PlayerScripts.Weapons.MeleeTrigger;
using Infrastructure.GameLoading;
using Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.PlayerScripts.Weapons
{
	[RequireComponent(typeof(AudioSource))]
	public abstract class AbstractWeapon : Item
	{
		[SerializeField] protected WeaponInfo WeaponInfo;

		private const string BaseLayer = "Base Layer";

		private DamageSoundPlayer _damageSoundPlayer;
		protected AudioSource WeaponAudio;
		protected AudioClip WeaponSound;
		protected Animator Animator;
		protected AnimationHasher AnimationHasher;
		protected bool IsRun;

		private AudioClip _damageSound;
		private IAssetProvider _assetProvider;
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

		public async UniTask Initialize(Animator animator, AnimatorFacade animatorFacade, AnimationHasher hasher,
			MeleeWeaponTrigger meleeWeaponTrigger)
		{
			_assetProvider = ServiceLocator.Container.GetSingle<IAssetProvider>();

			WeaponAudio = GetComponent<AudioSource>();
			_meleeWeaponTrigger = meleeWeaponTrigger;
			_animatorFacade = animatorFacade;
			Animator = animator;
			AnimationHasher = hasher;

			var damageSoundPlayer = await _assetProvider.Instantiate(WeaponInfo.DamageSoundPlayer.AssetGUID, transform.position);
			_damageSoundPlayer = damageSoundPlayer.GetComponent<DamageSoundPlayer>();
			_damageSound = await _assetProvider.LoadAsyncByGUID<AudioClip>(WeaponInfo.DamageSound.AssetGUID);
			WeaponSound = await _assetProvider.LoadAsyncByGUID<AudioClip>(WeaponInfo.WeaponSound.AssetGUID);
			
			CanAttack = true;
			Damage = WeaponInfo.Damage;
			AttackSpeed = WeaponInfo.AttackSpeed;
			Range = WeaponInfo.Range;

			Animator.SetFloat(AnimationHasher.AttackSpeedHash, AttackSpeed);
		}

		public void Use() =>
			Attack();

		public abstract void GiveDamage(IWeaponVisitor target);

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
			AttackAnimationEnded?.Invoke();
		}

		public void PlayDamageSound()
		{
			_damageSoundPlayer.Play(_damageSound);
		}
	}
}
using Infrastructure;
using Infrastructure.Services;
using PlayerScripts;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AnimationHasher))]
[RequireComponent(typeof(Player), typeof(WeaponFactory))]
[RequireComponent(typeof(PlayerWeapon))]
public class PlayerBehaviour : MonoBehaviour
{
	private Player _player;

	private PlayerWeapon _playerWeapon;
	private PhysicsMovement _physicsMovement;
	private AnimationHasher _animationHasher;
	private Animator _animator;
	private IInputService _inputService;

	private void Awake()
	{
		_inputService = ServiceLocator.Container.Single<IInputService>();
		_physicsMovement = GetComponent<PhysicsMovement>();
	}

	private void Start()
	{
		_playerWeapon = GetComponent<PlayerWeapon>();
		_player = GetComponent<Player>();
		_animationHasher = GetComponent<AnimationHasher>();
		_animator = GetComponent<Animator>();
	}
}
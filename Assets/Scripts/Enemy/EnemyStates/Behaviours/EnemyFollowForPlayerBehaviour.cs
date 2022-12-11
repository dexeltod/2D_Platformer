using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationHasher), typeof(Animator), typeof(Rigidbody2D))]
public class EnemyFollowForPlayerBehaviour : MonoBehaviour
{
	[SerializeField] private Player _player;
	[SerializeField] private DataEnemy _dataEnemy;
	[SerializeField] private EnemyMeleeRangeInformer _enemyMeleeInformer;

	public event UnityAction<bool> PlayerReached;

	private Rigidbody2D _rigidbody2D;
	private Vector2 _direction;
	private Animator _animator;
	private AnimationHasher _animationHasher;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_animationHasher = GetComponent<AnimationHasher>();
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		_enemyMeleeInformer.TouchedPlayer += OnTouchedPlayer;
		_animator.StopPlayback();
		_animator.CrossFade(_animationHasher.RunHash, 0);
	}

	private void OnDisable()
	{
		_enemyMeleeInformer.TouchedPlayer -= OnTouchedPlayer;
		_animator.StopPlayback();
	}

	private void FixedUpdate()
	{
		Vector2 direction = _player.transform.position - transform.position;
		_rigidbody2D.position += direction.normalized * (_dataEnemy.RunSpeed * Time.deltaTime);
	}

	private void OnTouchedPlayer(bool isReached) => 
		PlayerReached.Invoke(isReached);
}
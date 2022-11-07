using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class BloodParticleEnabler : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particleSystem;
	private Enemy _enemy;

	private void Awake() =>
		_enemy = GetComponent<Enemy>();

	private void OnEnable() =>
		_enemy.WasHit += PlayBoolParticle;

	private void OnDisable() =>
		_enemy.WasHit -= PlayBoolParticle;

	private void PlayBoolParticle() =>
		_particleSystem.Play();
}
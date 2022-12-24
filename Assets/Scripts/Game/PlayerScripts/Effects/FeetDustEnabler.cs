using Infrastructure;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class FeetDustEnabler : MonoBehaviour
{
	private PhysicsMovement _physicsMovement;
	private Transform _feetPosition;

	private IGameFactory _factory;
	private VisualEffect _visualEffect;

	private void Start()
	{
		_visualEffect = GetComponent<VisualEffect>();
		_factory = ServiceLocator.Container.Single<IGameFactory>();

		if (_factory.MainCharacter != null)
			Initialize();
		else
			_factory.MainCharacterCreated += OnHeroCreated;
	}

	private void OnHeroCreated() => 
		Initialize();

	private void Initialize()
	{
		_factory.MainCharacterCreated -= OnHeroCreated;
		_physicsMovement = _factory.MainCharacter.GetComponent<PhysicsMovement>();
		_feetPosition = _physicsMovement.FeetPosition;
		_physicsMovement.Running += ChangeEffectEnabling;
	}

	private void OnDisable()
	{
		if(_physicsMovement == null)
			return;
		
		_physicsMovement.Running -= ChangeEffectEnabling;
	}

	private void ChangeEffectEnabling(bool isRun)
	{
		if (isRun == false)
			return;

		var position = _feetPosition.transform.position;
		
		_visualEffect.transform.position = new Vector3(position.x, position.y, 0);
		_visualEffect.transform.rotation = _feetPosition.rotation;
		_visualEffect.Play();
	}
}
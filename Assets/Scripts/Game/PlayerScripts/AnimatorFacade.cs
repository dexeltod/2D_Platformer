using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.PlayerScripts
{
	public class AnimatorFacade : MonoBehaviour
	{
		private Animator _animator;
		private Coroutine _currentAnimationRoutine;
		private int _currentAnimationHash;

		private void Awake() =>
			_animator = GetComponent<Animator>();

		private void FixedUpdate()
		{
			// _animator.Play(_currentAnimationHash);
		}

		public void Play(int hash)
		{
			StopAnimationRoutine();
			_currentAnimationRoutine = StartCoroutine(StartAnimationRoutine(hash));
			// _currentAnimationHash = hash;
		}
		
		
		private void StopAnimationRoutine()
		{
			if (_currentAnimationRoutine != null)
				StopCoroutine(_currentAnimationRoutine);
		}
		
		private IEnumerator StartAnimationRoutine(int hash)
		{
			float timeOut = 1f;
			
			while (GetAnimatorInfo().shortNameHash != hash)
			{
				_animator.Play(hash);
				_animator.Update(Time.deltaTime);
				timeOut -= Time.deltaTime;
				
				if(timeOut <= 0)
					yield break;
				
				yield return 0;
			}
		}
		
		private AnimatorStateInfo GetAnimatorInfo() => 
			_animator.GetCurrentAnimatorStateInfo(0);
	}
}
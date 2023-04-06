using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DamageSoundPlayer : MonoBehaviour
{
	[SerializeField] private AudioSource _audioSource;

	public void Play(AudioClip clip)
	{
		_audioSource.enabled = true;
		_audioSource.clip = clip;
		_audioSource.Play();
	}
}
using UnityEngine;

public class SoundSetter : MonoBehaviour
{
	[SerializeField] private AudioSource _audio;

	private void Awake() => 
		DontDestroyOnLoad(this);

	public void SetAudioClip(AudioClip clip)
	{
		_audio.clip = clip;
		_audio.Play();
	}

	public void Stop() => 
		_audio.Stop();
}

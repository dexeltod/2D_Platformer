using UnityEngine;

public class GameStopper : MonoBehaviour
{
	[SerializeField] private InputSystemReader _inputSystemReader;
	
	private void OnEnable()
	{
		_inputSystemReader.enabled = false;
		const int PauseTime = 0;
		Time.timeScale = PauseTime;
	}

	private void OnDisable()
	{
		_inputSystemReader.enabled = true;
		const int NormalTime = 1;
		Time.timeScale = NormalTime;
	}
}
using UnityEngine;

public class GameStopper : MonoBehaviour
{
	private void OnEnable()
	{
		const int PauseTime = 0;
		Time.timeScale = PauseTime;
	}

	private void OnDisable()
	{
		const int NormalTime = 1;
		Time.timeScale = NormalTime;
	}
}
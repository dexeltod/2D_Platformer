using System;
using UnityEngine;

public class PlayerSceneSwitcher : MonoBehaviour
{
	public event Action SceneSwitched;

	public void SwitchState() => 
		SceneSwitched?.Invoke();
}

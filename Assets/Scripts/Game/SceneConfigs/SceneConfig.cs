using UnityEngine;

[CreateAssetMenu(fileName = "ConfigLevel_", menuName = "Data/Level/LevelConfig")]
public class SceneConfig : ScriptableObject
{
	[SerializeField] private string _name;
	[SerializeField] private string _music;
	[SerializeField] private bool _isStopMusicBetweenScenes;

	public string Name => _name;
	public string Music => _music;
	public bool IsStopMusicBetweenScenes => _isStopMusicBetweenScenes;
}

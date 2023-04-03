using UnityEngine;
using UnityEngine.Serialization;

namespace Game.SceneConfigs
{
	[CreateAssetMenu(fileName = "ConfigLevel_", menuName = "Data/Level/LevelConfig")]
	public class SceneConfig : ScriptableObject
	{
		[SerializeField] private string _name;
		[FormerlySerializedAs("_music")] [SerializeField] private string _musicName;
		[SerializeField] private bool _isStopMusicBetweenScenes;

		public string Name => _name;
		public string MusicName => _musicName;
		public bool IsStopMusicBetweenScenes => _isStopMusicBetweenScenes;
	}
}

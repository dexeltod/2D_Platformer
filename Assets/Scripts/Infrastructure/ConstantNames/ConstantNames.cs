using UnityEngine.Device;

namespace Infrastructure.ConstantNames
{
	public static class ConstantNames
	{
		public static readonly MusicNames MusicNames = new();
		public static readonly ProgressNames ProgressNames = new();

		public const string PlayerSpawnPointTag = "InitialPoint";
		public const string FirstLevel = "Level_1";
		public const string InitialScene = "Initial D";
		public const string MenuScene = "Main_Menu";
		public const string PlayerPrefabPath = "Prebafs/Game/Characters/MainCharacter/Hugo";
	}

	public class ProgressNames
	{
		public readonly string SaveFileFormat = ".data";
		public string SavesDirectory => Application.persistentDataPath + "/Saves/";
		public readonly string SaveName = "save_";
	}
}
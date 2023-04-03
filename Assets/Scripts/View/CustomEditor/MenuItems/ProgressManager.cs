using Infrastructure.ConstantNames;
using UnityEditor;
using Utils;

namespace View.CustomEditor.MenuItems
{
	public static class ProgressManager
	{
#if UNITY_EDITOR
		[MenuItem("Tools/Progress/ClearSaves")]
		private static void ClearProgress() =>
			FileManager.DeleteAllFilesInDirectory(ConstantNames.ProgressNames.SavesDirectory);
#endif
	}
}
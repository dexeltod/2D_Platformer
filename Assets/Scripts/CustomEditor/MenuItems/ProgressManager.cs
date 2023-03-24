using Infrastructure.ConstantNames;
using UnityEditor;
using Utils;

namespace CustomEditor.MenuItems
{
	public static class ProgressManager
	{
		[MenuItem("Tools/Progress/ClearSaves")]
		private static void ClearProgress() => 
			FileManagerFacade.DeleteAllFilesInDirectory(ConstantNames.ProgressNames.SavesDirectory);
	}
}
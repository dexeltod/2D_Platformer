using System.IO;
using System.Linq;

namespace Utils
{
	public static class FileManagerFacade
	{
		public static void DeleteAllFilesInDirectory(string directory)
		{
			string[] files = Directory.GetFiles(directory);
			
			foreach (var file in files) 
				File.Delete(file);
		}

		public static void DeleteFile(string directory, string file)
		{
			string[] files = Directory.GetFiles(directory);

			string foundedFile = files.Single(f => f == file);
			File.Delete(foundedFile);
		}
	}
}
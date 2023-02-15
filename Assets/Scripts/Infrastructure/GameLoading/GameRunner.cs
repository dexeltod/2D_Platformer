using UnityEngine;

namespace Infrastructure.GameLoading
{
	public class GameRunner : MonoBehaviour
	{
		[SerializeField] private Bootstrapper _bootstrapperPrefab;
	
		private void Awake()
		{
			var bootstrapper = FindObjectOfType<Bootstrapper>();

			if (bootstrapper == null) 
				Instantiate(_bootstrapperPrefab);
		}
	}
}
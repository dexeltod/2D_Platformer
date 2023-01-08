using UnityEngine;
using UnityEngine.Tilemaps;

namespace Infrastructure.Services
{
	public class PathFindService : MonoBehaviour
	{
		private Tilemap _tilemap;
		private Vector3Int _positionInCell;

		private void Awake()
		{
			_tilemap = GetComponent<Tilemap>();
		}

		private void Start()
		{
			_positionInCell = _tilemap.WorldToCell(transform.position);
		}
	}
}
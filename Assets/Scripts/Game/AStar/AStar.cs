using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar : MonoBehaviour
{
	private TileType _tileType;
	private Tilemap _tilemap;

	private void Awake()
	{
		_tilemap = GetComponent<Tilemap>();
	}

	private void Initialize()
	{
		
	}
}
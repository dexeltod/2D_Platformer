using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "AStar Tile", menuName = "Tiles/AStar")]
public class AStarTile : Tile
{
	[SerializeField] private int _g;
	[SerializeField] private int _h;
	
	public int RoadHeaviness => _g + _h;
}
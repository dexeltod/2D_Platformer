using System;
using UnityEngine;

public class EnemyPathfinder : MonoBehaviour
{
	private AStar _aStar;

	private void Start()
	{
		_aStar = GetComponent<AStar>();
	}

	public void FindPath()
	{
		
	}
}

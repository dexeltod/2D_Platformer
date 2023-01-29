using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
	[SerializeField] private GameObject _prefab;
	[SerializeField] private string _description;
	[SerializeField] private int _price;

	public GameObject Prefab => _prefab;
	public string Description => _description;
	public int Price => _price;
}
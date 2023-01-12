using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NormalMapSetter : MonoBehaviour
{
	[SerializeField] private Texture2D _textureNormal;

	private SuperCustomProperties _customProperties;
	private TilemapRenderer _tilemapRenderer;

	private void Start()
	{
		_customProperties = GetComponent<SuperCustomProperties>();
		_tilemapRenderer = GetComponent<TilemapRenderer>();

		Initialize();
	}

	private void Initialize()
	{
		CustomProperty customProperty;
		string propertyName = "_NormalMap";

		_customProperties.m_Properties.TryGetProperty(propertyName, out customProperty);

		_tilemapRenderer.material.EnableKeyword("_NORMALMAP");
		_tilemapRenderer.material.SetTexture("_BumpMap", _textureNormal);
		
	}
}
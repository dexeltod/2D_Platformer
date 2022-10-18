using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _height;

    private Transform _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerCharacter>().transform;
    }

    private void FixedUpdate()
    {
        UpdateCameraPositon();
    }

    void UpdateCameraPositon()
    {
        Vector3 position = new Vector3(_player.position.x, _player.position.y + _height);
        transform.position = position;
    }
}

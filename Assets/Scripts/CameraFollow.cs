using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _distance = -10;

    private Transform _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerEntity>().transform;
    }

    private void FixedUpdate()
    {
        UpdateCameraPositon();
    }

    void UpdateCameraPositon()
    {
        Vector3 position = new Vector3(_player.position.x, _player.position.y + _height, _distance);
        transform.position = position;
    }
}

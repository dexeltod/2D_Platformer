using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _height;
    [SerializeField] private float _distance = - 10;

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

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float _height;
    [SerializeField] private float _closeness = -30;

    private Transform _player;

    private void Start()
    {
        _player = GetComponentInParent<PlayerCharacter>().transform;
    }

    private void FixedUpdate()
    {
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        Vector3 position = new Vector3(_player.position.x, _player.position.y + _height, _closeness);
        transform.position = position;
    }
}

using UnityEngine;

public class EnterInLocation : MonoBehaviour
{
    [SerializeField] private InputSystemReader _inputSystemReader;
    [SerializeField] private Transform _placeTransform;
    [SerializeField] private Transform _playerTransform;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEntity player))
        {
            _inputSystemReader.ButtonUse += Enter;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _inputSystemReader.ButtonUse -= Enter;
    }

    private void Enter()
    {
        _playerTransform.position = _placeTransform.position;
    }
}

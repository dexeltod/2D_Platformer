using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private InputSystemReader _inputSystemReader;
    [SerializeField] private Transform _placeTransform;
    [SerializeField] private Transform _playerTransform;

    private int _useButtonValue = 1;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEntity player))
        {
            Enter();
        }
    }

    private void Enter()
    {
        if (_inputSystemReader.ButtonUseValue == _useButtonValue)
        {
            _playerTransform.position = _placeTransform.position;
            _useButtonValue = 0;
        }

    }
}

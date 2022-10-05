using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField] private InputSystemReader _inputSystemReader;
    [SerializeField] private Transform _placeTransform;
    [SerializeField] private Transform _playerTransform;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEntity player))
        {
            Enter();
        }
    }

    private void Enter()
    {
        int useButtonValue = 1;

        if (_inputSystemReader.ButtonUseValue == useButtonValue)
        {
            _playerTransform.position = _placeTransform.position;
        }

        Debug.Log(_inputSystemReader.ButtonUseValue);
    }
}

using UnityEngine;

public class EnterInLocation : MonoBehaviour
{    
    [SerializeField] private InputSystemReader _inputSystemReader;
    [SerializeField] private Transform _placeTransform;
    [SerializeField] private Transform _playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEntity player))
        {
            EnableButton();
        }
    }

    private void OnTriggerExit2D()
    {
        DisableButton();
    }

    private void EnableButton()
    {
        _inputSystemReader.ButtonUse += Enter;
    }

    private void DisableButton()
    {
        _inputSystemReader.ButtonUse -= Enter;
    }

    private void Enter()
    {
        DisableButton();
        _playerTransform.position = _placeTransform.position;
    }
}

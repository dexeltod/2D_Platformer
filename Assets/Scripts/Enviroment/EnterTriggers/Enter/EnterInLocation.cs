using UnityEngine;

public class EnterInLocation : MonoBehaviour
{    
    [SerializeField] private InputSystemReader _inputSystemReader;
    [SerializeField] private Transform _placeTransform;
    [SerializeField] private Transform _playerTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCharacter player))
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
        _inputSystemReader.InteractButtonUsed += Enter;
    }

    private void DisableButton()
    {
        _inputSystemReader.InteractButtonUsed -= Enter;
    }

    private void Enter()
    {
        DisableButton();
        _playerTransform.position = _placeTransform.position;
    }
}

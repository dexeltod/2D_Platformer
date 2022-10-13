using UnityEngine;

public class GoodCupboard : BaseCupboard
{
    [SerializeField] private InputSystemReader _inputSystemReader;

    private void Awake()
    {
        SetCupboard(new GoodCuboardBehavior());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCharacter player))
        {
            _inputSystemReader.InteractButtonUsed += Open;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerCharacter player))
        {
            _inputSystemReader.InteractButtonUsed -= Open;
        }
    }
}

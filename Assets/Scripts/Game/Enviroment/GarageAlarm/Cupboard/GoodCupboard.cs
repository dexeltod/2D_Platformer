using UnityEngine;

public class GoodCupboard : BaseCupboard
{
    [SerializeField] private InputSystemReaderService _inputSystemReaderService;

    private void Awake()
    {
        SetCupboard(new GoodCuboardBehavior());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            _inputSystemReaderService.InteractButtonUsed += Open;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerHealth player))
        {
            _inputSystemReaderService.InteractButtonUsed -= Open;
        }
    }
}

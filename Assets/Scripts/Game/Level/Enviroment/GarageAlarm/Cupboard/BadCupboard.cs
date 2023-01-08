using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BadCupboard : BaseCupboard
{
    [SerializeField] private AlarmIncreaser _alarm;
    [SerializeField] private InputSystemReaderService _inputSystemReaderService;

    private void Awake()
    {
        SetCupboard(new BadCupboardBehaviour(_alarm));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerHealth player))
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

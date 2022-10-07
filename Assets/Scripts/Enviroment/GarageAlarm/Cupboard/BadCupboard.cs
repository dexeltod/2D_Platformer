using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BadCupboard : BaseCupboard
{
    [SerializeField] private AlarmBarMover _barMover;
    [SerializeField] private InputSystemReader _inputSystemReader;

    private void Awake()
    {
        SetCupboard(new BadCupboardBehaviour(_barMover));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerEntity player))
        {
            _inputSystemReader.ButtonUse = Open;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerEntity player))
        {
            _inputSystemReader.ButtonUse -= Open;
        }
    }
}

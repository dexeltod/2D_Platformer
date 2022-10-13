using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BadCupboard : BaseCupboard
{
    [SerializeField] private AlarmIncreaser _alarm;
    [SerializeField] private InputSystemReader _inputSystemReader;

    private void Awake()
    {
        SetCupboard(new BadCupboardBehaviour(_alarm));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerCharacter player))
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

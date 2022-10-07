using UnityEngine;
using UnityEngine.Events;

public class BadCupboardBehaviour : ICupboard
{
    public UnityAction IsCupboardOpened;

    private AlarmBarMover _barMover;

    public BadCupboardBehaviour(AlarmBarMover barMover)
    {
        _barMover = barMover;  
    }

    public void Open()
    {
        _barMover.FillBar(1);
    }
}

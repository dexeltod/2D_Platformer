using UnityEngine;
using UnityEngine.Events;

public class GoodCuboardBehavior : ICupboard
{
    public UnityAction IsCupboardOpened;

    public void Open()
    {
       IsCupboardOpened?.Invoke();
    }
}

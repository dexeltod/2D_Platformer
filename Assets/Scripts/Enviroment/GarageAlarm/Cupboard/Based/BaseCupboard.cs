
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCupboard : MonoBehaviour
{
    [SerializeField] private UnityEvent IsOpened;

    protected ICupboard Cupboard;

    public void SetCupboard(ICupboard cupboard)
    {
        Cupboard = cupboard;
    }

    protected void Open()
    {
        Cupboard.Open();
    }
}

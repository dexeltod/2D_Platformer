
using UnityEngine;

public abstract class BaseCupboard : MonoBehaviour
{
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

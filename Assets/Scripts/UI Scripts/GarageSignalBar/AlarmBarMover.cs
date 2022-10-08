using UnityEngine;
using UnityEngine.UI;

public class AlarmBarMover : MonoBehaviour
{
    [SerializeField] private BadCupboardBehaviour _cupboard;
    [SerializeField] private Image _bar;

    public void FillBar(float fillAmount)
    {
        if(fillAmount >= 1.1f || fillAmount < -0.9f)
            return;

        _bar.fillAmount = fillAmount;
    }
}

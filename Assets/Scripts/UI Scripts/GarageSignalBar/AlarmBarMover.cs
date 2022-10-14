using UnityEngine;
using UnityEngine.UI;

public class AlarmBarMover : MonoBehaviour
{
    [SerializeField] private BadCupboardBehaviour _cupboard;
    [SerializeField] private Image _bar;

    public void FillBar(float fillAmount)
    {
        float maxFillAmount = 1.1f;
        float minFillAmount = -0.9f;

        if (fillAmount >= maxFillAmount || fillAmount < minFillAmount)
            return;

        _bar.fillAmount = fillAmount;
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlarmBarMover : MonoBehaviour
{
    [SerializeField] private BadCupboardBehaviour _cupboard;
    [SerializeField] private Image _bar;

    public void FillBar(float fillAmount)
    {
        if(fillAmount > 1 || fillAmount < 0)
            return;

        _bar.fillAmount = 1;
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class AlarmIncreaser : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AlarmBarMover _bar;
    [SerializeField, Range(0, 1)] private float _pointPerSecond;

    private float _currentAlarmValue = 0;
    private Coroutine _currentCoroutine;

    public void SetMaxAlarmVolume()
    {
        float maxVolume = 1;

        _currentAlarmValue = maxVolume;
        _audio.volume = maxVolume;
        _bar.FillBar(maxVolume);
    }

    public void StopMovingAlarmValues()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    private void OnTriggerEnter2D()
    {
        StopMovingAlarmValues();
        _currentCoroutine = StartCoroutine(MoveBar(_pointPerSecond));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopMovingAlarmValues();
        _currentCoroutine = StartCoroutine(MoveBar(-_pointPerSecond));
    }    

    private IEnumerator MoveBar(float point)
    {
        float maxAlarmValue = 1.1f;
        float minAlarmValue = 0f;
        float waitingValue = 1f;
        
        var waitingTime = new WaitForSeconds(waitingValue);

        while (_currentAlarmValue <= maxAlarmValue || _currentAlarmValue >= minAlarmValue)
        {
            _currentAlarmValue += point;

            _audio.volume = _currentAlarmValue;
            _bar.FillBar(_currentAlarmValue);
            yield return waitingTime;
        }
    }
}

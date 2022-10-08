using System.Collections;
using UnityEngine;

public class AlarmIncreaser : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AlarmBarMover _bar;
    [SerializeField, Range(0, 1)] private float _pointPerSecond;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(MoveBar(_pointPerSecond));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(MoveBar(_pointPerSecond));
        StartCoroutine(MoveBar(-_pointPerSecond));
    }

    IEnumerator MoveBar(float point)
    {
        float maxAlarmValue = 1.0f;
        float currentAlarmValue = 0f;
        float waitingValue = 1f;

        while (currentAlarmValue <= maxAlarmValue)
        {
            Debug.Log(currentAlarmValue);
            currentAlarmValue += point;

            _audio.volume = currentAlarmValue;
            _bar.FillBar(currentAlarmValue);
            yield return new WaitForSeconds(waitingValue);
        }        
    }
}

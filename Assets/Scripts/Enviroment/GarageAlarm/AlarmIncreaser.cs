using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmIncreaser : MonoBehaviour
{
    [SerializeField] private AlarmBarMover _bar;
    [Range(0,1)] float _alarmPointPerSecond;
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }


    private void IncreaseAlarm()
    {
        
        _bar.FillBar(_alarmPointPerSecond);
    }
}

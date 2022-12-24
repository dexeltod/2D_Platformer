using UnityEngine;


[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data Data/Detect state")]
public class DataEnemyDetectState : ScriptableObject
{
    public float RunSpeed = 1f;
    public float MaxDistanceToFollow = 5f;
}

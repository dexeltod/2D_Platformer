using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Data/EnemyEntity/EntityData")]
public class CharacterData : ScriptableObject
{
    public int Health;
    public float MoveSpeed;
}

using UnityEngine;

public class EnemyWinBehaviour : MonoBehaviour
{
    private Animator _animation;
    private EnemyAnimationHashes _moveHashes;

    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animation.Play(_moveHashes.MoveHash);
    }
}
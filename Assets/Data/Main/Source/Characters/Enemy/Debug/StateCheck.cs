using UnityEngine;
using TMPro;

public class StateCheck : MonoBehaviour
{
    private FiniteStateMachine _state;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _state = new FiniteStateMachine();
    }

    private void Update()
    {
        _textMeshPro.text = _state.CurrentState.ToString();
        Debug.Log(_state.CurrentState); 
    }
}

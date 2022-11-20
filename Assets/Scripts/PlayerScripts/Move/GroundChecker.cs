using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
	[SerializeField] private bool _isDebugEnable = true;
	[SerializeField] private Vector2 _groundCheckPosition;
	[SerializeField] private Vector2 _groundCheckSize;
	[SerializeField] private LayerMask _groundLayer;

	private Rigidbody2D _rigidbody2D;

	private bool _isGrounded;
	private bool _lastGroundedBool;

	public event UnityAction<bool> GroundedStateSwitched;

	private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

	private void Update() => CheckGround();

	private void CheckGround()
	{
		float groundCheckAngle = 0;

		_isGrounded = Physics2D.OverlapBox(_rigidbody2D.position + _groundCheckPosition,
			_groundCheckSize, groundCheckAngle, _groundLayer);

		SwitchGroundedBool();
	}

	private void SwitchGroundedBool()
	{
		if (_lastGroundedBool == _isGrounded)
			return;

		_lastGroundedBool = _isGrounded;
		GroundedStateSwitched?.Invoke(_lastGroundedBool);
	}

	private void OnDrawGizmos()
	{
		if (_isDebugEnable == true)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawCube(transform.position + (Vector3)_groundCheckPosition, _groundCheckSize);
		}
	}
}
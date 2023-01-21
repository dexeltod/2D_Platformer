using UnityEngine;

namespace PlayerScripts.TestStateMachine
{
	public interface IMovableState
	{
		void SetMoveDirection(Vector2 direction);
	}
}
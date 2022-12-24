using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputSystemReaderService
{
	public Action InteractButtonUsed;
	public Action AttackButtonPerformed;
	public Action VerticalMoveButtonCanceled;
	public Action JumpButtonUsed;
	public Action JumpButtonCanceled;

	public UnityAction<float> VerticalMoveButtonUsed;

	private float _buttonAttackValue;
	private float _buttonJumpValue;
	private readonly InputSystem _inputActions;

	public InputSystemReaderService()
	{
		_inputActions = new InputSystem();
		SubscribeOnButtons();
	}

	public void DisableInputs()
	{
		
	}

	public void EnableInputs()
	{
		_inputActions.Enable();
	}
	
	private void SubscribeOnButtons()
	{
		_inputActions.Player.Move.performed += OnHorizontalMovement;
		_inputActions.Player.Jump.performed += OnJump;
		_inputActions.Player.Attack.performed += OnAttack;
		_inputActions.Player.Use.started += OnUse;
	}

	private void OnUse(InputAction.CallbackContext context)
	{
		if (context.started)
			InteractButtonUsed?.Invoke();
	}


	private void OnAttack(InputAction.CallbackContext context)
	{
		if (context.performed)
			AttackButtonPerformed?.Invoke();
	}

	private void OnHorizontalMovement(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			var direction = context.ReadValue<float>();
			VerticalMoveButtonUsed?.Invoke(direction);
		}

		if (context.canceled) 
			VerticalMoveButtonCanceled?.Invoke();
	}

	private void OnJump(InputAction.CallbackContext context)
	{
		if (context.started)
			JumpButtonUsed?.Invoke();

		if (context.canceled)
			JumpButtonCanceled?.Invoke();
	}
}
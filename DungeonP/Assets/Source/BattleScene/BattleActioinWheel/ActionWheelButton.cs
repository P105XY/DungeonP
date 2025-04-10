using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

//ActionWheel에서 action 버튼이 입력될 경우 사용될 기능을 구현한 클래스.
public class ActionWheelButton : ActionWheelButtonBase, IWheelButtonHandler
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        EnterActionInput.started += OnWheelButtonClicked;
        ArrowKeyInput.started += OnWheelButtonHovered;
        ExitActionInput.started += OnCancelButtonClicked;
    }

    private void OnDisable()
    {
        EnterActionInput.started -= OnWheelButtonClicked;
        ArrowKeyInput.started -= OnWheelButtonHovered;
        ExitActionInput.started -= OnCancelButtonClicked;
    }

    public void OnWheelButtonClicked(InputAction.CallbackContext context)
    {
    }

    public void OnWheelButtonHovered(InputAction.CallbackContext context)
    {

    }

    public void OnCancelButtonClicked(InputAction.CallbackContext context)
    {
    }
}

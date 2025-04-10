using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

interface IWheelButtonHandler
{
    void OnWheelButtonClicked(InputAction.CallbackContext context);
    void OnWheelButtonHovered(InputAction.CallbackContext context);
    void OnCancelButtonClicked(InputAction.CallbackContext context);
}

//action wheel에서 사용할 버튼 위젯의 기본 기능 구현.
public abstract class ActionWheelButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button ActionButton;
    private BattleController battleController;
    private RectTransform rectTransform;
    private bool bIsCursurHover;
    private Vector2 initVector;
    private Coroutine floatingCoroutine;

    protected InputAction EnterActionInput;
    protected InputAction ArrowKeyInput;
    protected InputAction ExitActionInput;
    protected InputAction InteractionInput;
    protected MovementAction movementActionClass;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initVector = rectTransform.anchoredPosition;
        bIsCursurHover = false;

        ActionButton = GetComponent<Button>();
        if (ActionButton == null)
        {
            return;
        }
    }

    private void Update()
    {
        if (bIsCursurHover && floatingCoroutine == null)
        {
            floatingCoroutine = StartCoroutine(IconFloating());
        }
        else if (!bIsCursurHover && floatingCoroutine != null)
        {
            StopCoroutine(floatingCoroutine);
            floatingCoroutine = null;
            rectTransform.anchoredPosition = initVector;
        }
    }

    private void OnEnable()
    {
        movementActionClass = new MovementAction();
        EnterActionInput = movementActionClass.Movement.Enter;
        ArrowKeyInput = movementActionClass.Movement.Movement;
        ExitActionInput = movementActionClass.Movement.Cancel;
        InteractionInput = movementActionClass.Movement.Interact;

        movementActionClass.Enable();
        EnterActionInput.Enable();
        ArrowKeyInput.Enable();
        ExitActionInput.Enable();
        InteractionInput.Enable();
    }

    private void OnDisable()
    {
        movementActionClass.Disable();
        EnterActionInput.Disable();
        ArrowKeyInput.Disable();
        ExitActionInput.Disable();
        InteractionInput.Disable();

        if (ActionButton == null)
        {
            return;
        }

        ActionButton.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null)
        {
            throw new System.NotImplementedException();
        }

        bIsCursurHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
        {
            throw new System.NotImplementedException();
        }

        bIsCursurHover = false;
    }

    private IEnumerator IconFloating()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            float amplitude = 10f; // 기본 진폭
            float frequency = 2.0f * Mathf.PI / 5.0f;
            float sineValue = amplitude * Mathf.Sin(Time.time * frequency); //time * 진동 주기.
            rectTransform.anchoredPosition = new Vector2(initVector.x, initVector.y + sineValue);
        }
    }
}

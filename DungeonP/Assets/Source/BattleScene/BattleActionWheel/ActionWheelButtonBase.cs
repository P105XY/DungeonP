using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

//action wheel에서 사용할 버튼 위젯의 기본 기능 구현.
public abstract class ActionWheelButtonBase : MonoBehaviour
{
    private BattleController battleController;
    private RectTransform rectTransform;
    private bool bIsCursurHover;
    private Vector2 initVector;
    private Coroutine floatingCoroutine;
    protected UnityEngine.UI.Button ActionButton;
    protected Navigation navigationMoveEvent;

    public MovementAction movementAction;
    public InputAction InteractAction;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initVector = rectTransform.anchoredPosition;
        bIsCursurHover = false;

        ActionButton = GetComponent<UnityEngine.UI.Button>();
        if (ActionButton == null)
        {
            return;
        }
    }

    private void Update()
    {
        if (this.gameObject == EventSystem.current.currentSelectedGameObject)
        {
            bIsCursurHover = true;
        }
        else
        {
            bIsCursurHover = false;
        }

        if (bIsCursurHover)
        {
            floatingCoroutine = StartCoroutine(IconFloating());
        }
        else
        {
            if (floatingCoroutine != null)
            {
                StopCoroutine(floatingCoroutine);
            }
            floatingCoroutine = null;
            rectTransform.anchoredPosition = initVector;
        }
    }

    protected virtual void OnEnable()
    {
        movementAction = new MovementAction();
        movementAction.Enable();

        InteractAction = movementAction.Movement.Interact;
        InteractAction.Enable();
        InteractAction.started += ClickInteractButton;

        if (ActionButton == null)
        {
            ActionButton = GetComponent<UnityEngine.UI.Button>();
        }

        ActionButton.onClick.AddListener(OnClickWheelButton);
    }

    protected virtual void OnDisable()
    {
        movementAction.Disable();
        InteractAction.started -= ClickInteractButton;
        InteractAction.Disable();

        if (ActionButton == null)
        {
            ActionButton = GetComponent<UnityEngine.UI.Button>();
        }

        ActionButton.onClick.RemoveAllListeners();
    }

    public void SetIsHover(bool bInIssHover)
    {
        bIsCursurHover = bInIssHover;
    }

    public void ClickInteractButton(InputAction.CallbackContext context)
    {
        if (context.action == null)
        {
            return;
        }

        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            OnClickWheelButton();
        }
    }

    public abstract void OnClickWheelButton();

    //방향키를 사용해서 selected가 되면, 해당 버튼이 둥둥 떠다니는것 같은 액션을 취함.
    private IEnumerator IconFloating()
    {
        if (floatingCoroutine != null)
        {
            yield return null;
        }

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

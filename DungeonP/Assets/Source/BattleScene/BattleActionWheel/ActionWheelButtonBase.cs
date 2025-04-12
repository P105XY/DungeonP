using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

interface IWheelButtonHandler
{
    void OnWheelButtonClicked(InputAction.CallbackContext context);
    void OnWheelButtonHovered(InputAction.CallbackContext context);
    void OnCancelButtonClicked(InputAction.CallbackContext context);
}

//action wheel에서 사용할 버튼 위젯의 기본 기능 구현.
public abstract class ActionWheelButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private BattleController battleController;
    private RectTransform rectTransform;
    private bool bIsCursurHover;
    private Vector2 initVector;
    private Coroutine floatingCoroutine;
    protected UnityEngine.UI.Button ActionButton;
    protected Navigation navigationMoveEvent;

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
    }

    protected virtual void OnDisable()
    {
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

        SetIsHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData == null)
        {
            throw new System.NotImplementedException();
        }

        SetIsHover(false);
    }

    public void SetIsHover(bool bInIssHover)
    {
        bIsCursurHover = bInIssHover;
    }

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

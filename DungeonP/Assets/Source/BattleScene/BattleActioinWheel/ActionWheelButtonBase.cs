using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//action wheel에서 사용할 버튼 위젯의 기본 기능 구현.
public abstract class ActionWheelButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button ActionButton;
    private BattleController battleController;
    private RectTransform rectTransform;
    private bool bIsCursurHover;
    private Vector2 initVector;
    private Coroutine floatingCoroutine;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initVector = rectTransform.anchoredPosition;

        ActionButton = GetComponent<Button>();
        if (ActionButton == null)
        {
            return;
        }

        ActionButton.onClick.AddListener(this.OnClick);
    }

    private void Update()
    {
        if (bIsCursurHover && floatingCoroutine == null)
        {
            floatingCoroutine = StartCoroutine(IconFloating());
        }
        else
        {
            StopCoroutine(floatingCoroutine);
            floatingCoroutine = null;
            rectTransform.anchoredPosition = initVector;
        }
    }

    void OnDisable()
    {
        ActionButton.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {

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
            yield return null;
            Vector2 currentRect = rectTransform.anchoredPosition;
            float amplitude = 10f; // 기본 진폭
            float sineValue = amplitude * Mathf.Sin(Time.time);
            rectTransform.anchoredPosition = new Vector2(currentRect.x, currentRect.y + sineValue);
        }
    }
}

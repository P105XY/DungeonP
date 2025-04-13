using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

//ActionWheel에서 action 버튼이 입력될 경우 사용될 기능을 구현한 클래스.
public class ActionWheelButton : ActionWheelButtonBase
{
    public override void OnClickWheelButton()
    {
        Debug.Log("Click action Button");
    }
}

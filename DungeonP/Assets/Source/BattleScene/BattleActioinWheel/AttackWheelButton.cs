using UnityEngine;
using UnityEngine.UI;

public class AttackWheelButton : ActionWheelButtonBase
{
    private Navigation buttonNavigation;

    private void Start() {
        buttonNavigation = GetComponent<Button>().navigation;
        buttonNavigation.mode = Navigation.Mode.Explicit;

        buttonNavigation.selectOnLeft = GetComponent<Button>();
    }

}

using UnityEngine;
using UnityEngine.UI;

public class AttackWheelButton : ActionWheelButtonBase
{
    public void SelectToAttackTarget()
    {
        Transform parentTransform = transform.parent;
        GameObject parentObject = parentTransform.gameObject;

        
    }

    public override void OnClickWheelButton()
    {
Debug.Log("Click Attack Button");
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AttackWheelButton : ActionWheelButtonBase
{
    private void Start()
    {
        ActionButton.onClick.AddListener(SelectToAttackTarget);
    }

    public void SelectToAttackTarget()
    {
        Transform parentTransform = transform.parent;
        GameObject parentObject = parentTransform.gameObject;

        
    }
}

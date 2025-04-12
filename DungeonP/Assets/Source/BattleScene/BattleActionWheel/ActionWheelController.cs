using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ActionWheelController : MonoBehaviour
{
   private UnityEngine.UI.Image currentActionCharacterPortrait;


    void Start()
    {
        if(!gameObject.TryGetComponent<UnityEngine.UI.Image>(out currentActionCharacterPortrait))
        {
            return;
        }


    }

    void Update()
    {
        
    }
}

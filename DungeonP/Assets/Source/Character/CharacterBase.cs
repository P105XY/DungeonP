using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//탐험모드에서 사용할 캐릭터의 기본 클래스.
public abstract class CharacterBase : MonoBehaviour
{
    private CharacterBackpack CharacterBackpack;
    private GameObject characterObject;

    public ECharacterBorn eCharacterBorn;
    public Canvas InventoryCanvas;
    public Canvas EquipmentCanvas;

    public virtual void Awake()
    {
        characterObject = this.gameObject;
    }

    public virtual void Start()
    {
        if (InventoryCanvas is null)
        {
            return;
        }

        if (EquipmentCanvas is null)
        {
            return;
        }
    }
}

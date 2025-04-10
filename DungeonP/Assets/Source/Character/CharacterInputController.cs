using UnityEngine;
using UnityEngine.InputSystem;

//캐릭터의 inputaction을 통해서 입력을 받고, 해당 액션을 취하는 클래스.
public class CharacterInputController : MonoBehaviour
{
    private InputAction movementInputAction;
    private MovementAction movementActionClass;
    private InputAction characterInventoryAction;
    private InputAction characterInteractAction;
    private InputAction characterRunningAction;
    private InputAction characterCancelAction;
    private InputAction characterPauseAction;
    private Rigidbody2D rigid2D;
    private UnityEngine.Vector2 MovementVector;
    private CharacterBase characterBase;
    private FCharacterStatus characterStatus;
    private GameObject InventoryCanvas;
    private GameObject EquipmenetCanvas;

    private void Awake()
    {
        movementActionClass = new MovementAction();
        movementInputAction = movementActionClass.Movement.Movement;
        characterInteractAction = movementActionClass.Movement.Interact;
        characterRunningAction = movementActionClass.Movement.Run;
        characterCancelAction = movementActionClass.Movement.Cancel;
        characterPauseAction = movementActionClass.Movement.GamePause;
        characterInventoryAction = movementActionClass.Movement.InventorySwitch;
    }

    private void Start()
    {
        if (!TryGetComponent<Rigidbody2D>(out rigid2D))
        {
            return;
        }

        if (!TryGetComponent<CharacterBase>(out characterBase))
        {
            return;
        }

        characterStatus = characterBase.GetCharacterStat();

        InventoryCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.InventoryCanvasTagString);
        EquipmenetCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.EquipmentCanvas);
    }

    private void Update()
    {
        MovementVector = movementInputAction.ReadValue<UnityEngine.Vector2>();
        if (MovementVector != UnityEngine.Vector2.zero)
        {
            Movement(MovementVector * 100.0f);
        }
    }

    private void OnEnable()
    {
        movementInputAction.Enable();
        characterInteractAction.Enable();
        characterInteractAction.started += Interact;

        characterRunningAction.Enable();
        characterRunningAction.started += RunningStart;
        characterRunningAction.started += RunningStop;

        characterCancelAction.Enable();
        characterCancelAction.started += CancelAction;

        characterPauseAction.Enable();
        characterPauseAction.started += GamePause;

        characterInventoryAction.Enable();
        characterInventoryAction.started += InventorySwitch;
    }

    private void OnDisable()
    {
        movementInputAction.Disable();
        characterInteractAction.Disable();
        characterInteractAction.started -= Interact;

        characterRunningAction.Disable();
        characterRunningAction.started -= RunningStart;
        characterRunningAction.started -= RunningStop;

        characterCancelAction.Disable();
        characterCancelAction.started -= CancelAction;

        characterPauseAction.Disable();
        characterPauseAction.started -= GamePause;

        characterInventoryAction.Disable();
        characterInventoryAction.started -= InventorySwitch;
    }

    void InventorySwitch(InputAction.CallbackContext context)
    {
        Canvas inventoryUICanvas;
        if(!InventoryCanvas.TryGetComponent<Canvas>(out inventoryUICanvas))
        {
            return;
        }

        Canvas equipmentUICanvas;
        if(!EquipmenetCanvas.TryGetComponent<Canvas>(out equipmentUICanvas))
        {
            return;
        }

        inventoryUICanvas.enabled = !inventoryUICanvas.enabled;
        equipmentUICanvas.enabled = !equipmentUICanvas.enabled;
    }

    void Interact(InputAction.CallbackContext context)
    {

    }

    void CancelAction(InputAction.CallbackContext context)
    {

    }

    void RunningStart(InputAction.CallbackContext context)
    {

    }

    void RunningStop(InputAction.CallbackContext context)
    {

    }

    void GamePause(InputAction.CallbackContext context)
    {

    }

    void Movement(in UnityEngine.Vector2 moveVector)
    {
        if (rigid2D is null)
        {
            return;
        }

        rigid2D.MovePosition(rigid2D.position + moveVector * Time.deltaTime);
    }

}

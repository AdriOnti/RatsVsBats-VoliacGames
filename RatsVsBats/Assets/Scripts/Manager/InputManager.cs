using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get { return instance; }
    }

    private PlayerControls playerControls;

    // Jump Event
    public delegate void OnPlayerJump();
    public static event OnPlayerJump PlayerJump;

    // Aim
    public delegate void OnPlayerAim();
    public static event OnPlayerAim PlayerAim;

    // Crouch
    public delegate void OnPlayerCrouch();
    public static event OnPlayerCrouch PlayerCrouch;

    // Climb
    public delegate void OnPlayerClimb();
    public static event OnPlayerClimb PlayerClimb;

    // Stealth
    public delegate void OnPlayerStealth();
    public static event OnPlayerStealth PlayerStealth;

    // Attack
    public delegate void OnPlayerAttack();
    public static event OnPlayerAttack PlayerAttack;

    // Change Item
    public delegate void OnPlayerChangeItem();
    public static event OnPlayerChangeItem PlayerChangeItem;

    // Drop Item
    public delegate void OnPlayerDrop();
    public static event OnPlayerDrop PlayerDrop;

    // Interact
    public delegate void OnPlayerInteract();
    public static event OnPlayerInteract PlayerInteract;

    // Map
    public delegate void OnPlayerMap();
    public static event OnPlayerMap PlayerMap;

    // Pause Game
    public delegate void OnPlayerPause();
    public static event OnPlayerPause PlayerPause;

    // Inventory
    public delegate void OnPlayerInventory();
    public static event OnPlayerInventory PlayerInventory;

    private void Awake()
    {
        if(instance != null && instance != this) Destroy(gameObject);
        else instance = this;
        playerControls = new PlayerControls();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerControls.Game.Jump.started += _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed += _ => PlayerAim.Invoke();
        playerControls.Game.Crouch.performed += _ => PlayerCrouch.Invoke();
        playerControls.Game.Climb.performed += _ => PlayerClimb.Invoke();
        playerControls.Game.Stealth.performed += _ => PlayerStealth.Invoke();
        playerControls.Game.Attack.performed += _ => PlayerAttack.Invoke();
        playerControls.Game.ChangeItem.performed += _ => PlayerChangeItem.Invoke();
        playerControls.Game.DropItem.performed += _ => PlayerDrop.Invoke();
        playerControls.Game.Interact.performed += _ => PlayerInteract.Invoke();
        playerControls.Game.Map.performed += _ => PlayerMap.Invoke();
        playerControls.Game.Pause.performed += _ => PlayerPause.Invoke();
        playerControls.Game.Inventory.performed += _ => PlayerInventory.Invoke();
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Game.Jump.started -= _ => PlayerJump.Invoke();
        playerControls.Game.Aim.performed -= _ => PlayerAim.Invoke();
        playerControls.Game.Crouch.performed -= _ => PlayerCrouch.Invoke();
        playerControls.Game.Climb.performed -= _ => PlayerClimb.Invoke();
        playerControls.Game.Stealth.performed -= _ => PlayerStealth.Invoke();
        playerControls.Game.Attack.performed -= _ => PlayerAttack.Invoke();
        playerControls.Game.ChangeItem.performed -= _ => PlayerChangeItem.Invoke();
        playerControls.Game.DropItem.performed -= _ => PlayerDrop.Invoke();
        playerControls.Game.Interact.performed -= _ => PlayerInteract.Invoke();
        playerControls.Game.Map.performed -= _ => PlayerMap.Invoke();
        playerControls.Game.Pause.performed -= _ => PlayerPause.Invoke();
        playerControls.Game.Inventory.performed -= _ => PlayerInventory.Invoke();
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Game.Movement.ReadValue<Vector2>();
    }

    public float GetMouseScroll()
    {
        return playerControls.Game.ChangeItem.ReadValue<float>();
    }
}

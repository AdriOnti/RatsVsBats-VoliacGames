using System.Collections;
using UnityEngine;

public class Test_PlayerController : MonoBehaviour
{
    private static Test_PlayerController instance;
    public static Test_PlayerController Instance
    {
        get { return instance; }
    }

    private InputManager inputManager;
    private Vector2 movementInput;

    [Header("Test Inputs")]
    public bool isWalking;
    public bool isAiming;
    public bool isCrouching;
    public bool isStealthing;
    public bool isJumping;
    public bool isClimbing;
    public bool isAttacking;
    public float isChangingItem;
    public bool isDropingItem;
    public bool isInteracting;
    public bool map;
    public bool pause;

    [Space]
    public GameObject testPauseMenu;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        InputManager.PlayerJump += Jump;
        InputManager.PlayerAim += Aim;
        InputManager.PlayerCrouch += Crouch;
        InputManager.PlayerStealth += Stealth;
        InputManager.PlayerClimb += Climb;
        InputManager.PlayerAttack += Attack;
        InputManager.PlayerChangeItem += ChangeItem;
        InputManager.PlayerDrop += DropItem;
        InputManager.PlayerInteract += Interact;
        InputManager.PlayerMap += OpenCloseMap;
        InputManager.PlayerPause += PauseGame;
    }

    private void OnDisable()
    {
        InputManager.PlayerJump -= Jump;
        InputManager.PlayerAim -= Aim;
        InputManager.PlayerCrouch -= Crouch;
        InputManager.PlayerStealth -= Stealth;
        InputManager.PlayerClimb -= Climb;
        InputManager.PlayerAttack -= Attack;
        InputManager.PlayerChangeItem -= ChangeItem;
        InputManager.PlayerDrop -= DropItem;
        InputManager.PlayerInteract -= Interact;
        InputManager.PlayerMap -= OpenCloseMap;
        InputManager.PlayerPause -= PauseGame;
    }

    private void Update()
    {
        DetectMovement();
    }

    private void DetectMovement()
    {
        movementInput = inputManager.GetPlayerMovement();
        CheckIfIsMoving();
    }

    private void CheckIfIsMoving()
    {
        if (!(movementInput.x != 0 || movementInput.y != 0)) StartCoroutine(WaitForBoolToChange());
        else isWalking = true;
    }

    private IEnumerator WaitForBoolToChange()
    {
        StopCoroutine(WaitForBoolToChange());
        yield return new WaitForSeconds(0.1f);
        isWalking = false;
    }

    public void Jump()
    {
        isJumping = !isJumping;
    }

    public void Aim()
    {
        isAiming = !isAiming;
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
    }

    public void Stealth()
    {
        isStealthing = !isStealthing;
    }

    public void Climb()
    {
        isClimbing = !isClimbing;
    }

    public void Attack()
    {
        isAttacking = !isAttacking;
    }

    public void ChangeItem()
    {
        isChangingItem = inputManager.GetMouseScroll();
        if (isChangingItem > 0) Debug.Log("Scroll Up");
        if (isChangingItem < 0) Debug.Log("Scroll Down");
    }

    public void DropItem()
    {
        isDropingItem = !isDropingItem;
    }

    public void Interact()
    {
        isInteracting = !isInteracting;
    }

    public void OpenCloseMap()
    {
        map = !map;
    }

    public void PauseGame()
    {
        pause = !pause;
        if(pause) { testPauseMenu.SetActive(true); }
        else testPauseMenu.SetActive(false);
    }
}

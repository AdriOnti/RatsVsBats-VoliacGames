using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    // Singleton
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance; }
    }

    private InputManager inputManager;
    private Vector2 movementInput;
    private Rigidbody _rb;
    private Vector3 _velocity;

    [Header("Bools & Test Bools")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isWalking;
    [HideInInspector] private bool isAiming;
    [HideInInspector] private bool isCrouching;
    [HideInInspector] private bool isStealthing;
    [SerializeField] private bool isJumping;
    [HideInInspector] private bool isClimbing;
    [HideInInspector] private bool isAttacking;
    [SerializeField] private float isChangingItem;
    [HideInInspector] private bool isInteracting;

    // Public Variables
    [Header("Stadistics")]
    public float jumpForce;
    public float speed;
    public Vector3 _playerCamera;

    [Header("Items")]
    public Item actualItem;
    public int inventoryIndex;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
        _playerCamera = Camera.main.transform.forward;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        _rb = GetComponent<Rigidbody>();
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
        InputManager.PlayerInteract += Interact;
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
        InputManager.PlayerInteract -= Interact;
    }

    private void Update()
    {
        DetectMovement();
        DetectJump();

        Move();
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

    private void Move()
    {
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (movementInput.x != 0.0f || movementInput.y != 0.0f)
        {
            Vector3 direction = transform.forward * movementInput.y + transform.right * movementInput.x;
            _rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    private void DetectJump()
    {
        isGrounded = IsGrounded();
        if (isGrounded) isJumping = false;
    }

    public void Jump()
    {
        if (isGrounded && !isCrouching)
        {
            isJumping = true;
            _rb.velocity = Vector3.up * jumpForce;
        }
    }

    private bool IsGrounded()
    {
        return _rb.velocity.y == 0;
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
        int itemCount = InventoryManager.Instance.Items.Count;

        // If itemCount is 0, the actualItem on the hud would be null
        if (itemCount == 0)
        {
            actualItem = null;
            GameManager.Instance.UpdateItem(actualItem);
            return;
        }

        // Obtain the mouse scroll
        isChangingItem = inputManager.GetMouseScroll();

        // If the mouse scroll is greater than 0, add 1 to the inventory index
        if (isChangingItem > 0) inventoryIndex = (inventoryIndex + 1) % itemCount;
        // If the mouse scroll is lesser than 0, rest 1 to the inventory index
        else if (isChangingItem < 0) inventoryIndex = (inventoryIndex - 1 + itemCount) % itemCount;

        actualItem = InventoryManager.Instance.Items[inventoryIndex];
        GameManager.Instance.UpdateItem(actualItem);
    }

    public void Interact()
    {
        isInteracting = !isInteracting;
    }
}

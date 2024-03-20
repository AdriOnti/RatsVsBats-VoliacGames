using System.Collections;
using UnityEngine;

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

    [Header("Bools")]
    [SerializeField] private bool isGrounded;
    [HideInInspector] private bool isWalking;
    [HideInInspector] private bool isAiming;
    [HideInInspector] private bool isCrouching;
    [HideInInspector] private bool isStealthing;
    [HideInInspector] private bool isJumping;
    [HideInInspector] private bool isClimbing;
    [HideInInspector] private bool isAttacking;
    [HideInInspector] private bool isDroppingItem;
    [HideInInspector] private float isChangingItem;
    [HideInInspector] private bool isInteracting;

    // Public Variables
    [Header("Stadistics")]
    public float jumpForce;
    public float speed;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
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
        InputManager.PlayerDrop += DropItem;
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
        InputManager.PlayerDrop -= DropItem;
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
        isChangingItem = inputManager.GetMouseScroll();
        if (isChangingItem > 0) Debug.Log("Scroll Up");
        if (isChangingItem < 0) Debug.Log("Scroll Down");
    }

    public void DropItem()
    {
        isDroppingItem = !isDroppingItem;
    }

    public void Interact()
    {
        isInteracting = !isInteracting;
    }
}

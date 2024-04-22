using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Character
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
    [SerializeField] private float isChangingItem;
    [HideInInspector] private bool isInteracting;

    // Public Variables
    [Header("Stadistics")]
    public float originalSpeed;
    public Vector3 _playerCamera;

    [Header("Items")]
    public Item actualItem;
    public int inventoryIndex;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
        _playerCamera = Camera.main.transform.forward;
        originalSpeed = speed;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        _rb = GetComponent<Rigidbody>();
        if(currentHP <= 0 || currentHP > hp) currentHP = hp;
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
        CheckHP();

        if (Input.GetKeyUp(KeyCode.J)) currentHP -= 1;
    }

    private void CheckHP()
    {
        if (currentHP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        FadeManager.Instance.FadeOut();
        yield return new WaitForSecondsRealtime(1.5f);
        DataManager.Instance.LoadGame();
        yield return new WaitForSecondsRealtime(2f);
        FadeManager.Instance.FadeIn();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ItemPickup>(out ItemPickup ip))
        {
            ip.Collected();
        }
    }
}

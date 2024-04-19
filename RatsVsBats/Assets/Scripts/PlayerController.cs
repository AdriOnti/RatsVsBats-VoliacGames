using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

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
    private CharacterController characterController;
    private Rigidbody rb;
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
    [HideInInspector] private bool isDroppingItem;
    [SerializeField] private float isChangingItem;
    [HideInInspector] private bool isInteracting;

    // Public Variables
    [Header("Stadistics")]
    public float jumpForce;
    public float speed;
    public Transform playerCamera;
    public float groundDistance = 5f;
    public LayerMask groundMask;

    private float gravity = -9.81f;
    private float fallSpeed;
    private float verticalSpeed;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        characterController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        fallSpeed = 0f;
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
    }

    private void DetectMovement()
    {
        movementInput = inputManager.GetPlayerMovement();
        if (movementInput.magnitude > 0.1f)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = transform.forward * movementInput.y + transform.right * movementInput.x;
        Vector3 cameraDirection = playerCamera.forward;
        cameraDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(cameraDirection);
        transform.rotation = rotation;

        characterController.Move(direction.normalized * speed * Time.deltaTime);
    }

    private void DetectJump()
    {
        isGrounded = characterController.isGrounded;
        //Debug.Log(isGrounded);

        if (isGrounded)
        {
            isJumping = false;
        }
    }

    public void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartCoroutine(PerformJump());
        }
    }

    IEnumerator PerformJump()
    {
        float currentVerticalSpeed = 0f;

        Debug.Log(isGrounded);

        while (currentVerticalSpeed < jumpForce)
        {
            currentVerticalSpeed += Time.deltaTime * jumpForce * 10;
            characterController.Move(new Vector3(0, currentVerticalSpeed, 0) * Time.deltaTime);
            yield return null;
        }

        while (!isGrounded)
        {
            currentVerticalSpeed += gravity * Time.deltaTime * 2;
            characterController.Move(new Vector3(0, currentVerticalSpeed, 0) * Time.deltaTime);
            yield return null;
        }

        isJumping = false;
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
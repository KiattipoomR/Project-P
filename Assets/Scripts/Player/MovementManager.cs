using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour, PlayerControls.IPlayerActions, PlayerControls.IMenuActions
{
    // Components
    private Transform tf => GetComponent<Transform>();
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    // Player attributes
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int staminaConsumeRate = 1;
    private PlayerControls playerControls;
    private Vector2 playerMovement;
    private Vector2 bedPosition;

    // Animations and states
    private string currentAnimationState;

    private const string PLAYER_IDLE_FRONT = "Player_Idle_Front";
    private const string PLAYER_IDLE_BACK = "Player_Idle_Back";
    private const string PLAYER_IDLE_RIGHT = "Player_Idle_Right";
    private const string PLAYER_IDLE_LEFT = "Player_Idle_Left";
    private const string PLAYER_WALK_UP = "Player_Walk_Up";
    private const string PLAYER_WALK_DOWN = "Player_Walk_Down";
    private const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    private const string PLAYER_WALK_LEFT = "Player_Walk_Left";


    // Start is called before the first frame update
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.SetCallbacks(this);
        playerControls.Menu.SetCallbacks(this);
        bedPosition = tf.position;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        playerMovement = context.ReadValue<Vector2>();

        if (playerMovement.x > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
        }
        else if (playerMovement.x < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
        }
        else if (playerMovement.y > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP);
        }
        else if (playerMovement.y < 0)
        {
            ChangeAnimationState(PLAYER_WALK_DOWN);
        }
        else
        {
            string newAnimationState;
            switch (currentAnimationState)
            {
                case PLAYER_WALK_UP:
                    newAnimationState = PLAYER_IDLE_BACK;
                    break;
                case PLAYER_WALK_DOWN:
                    newAnimationState = PLAYER_IDLE_FRONT;
                    break;
                case PLAYER_WALK_RIGHT:
                    newAnimationState = PLAYER_IDLE_RIGHT;
                    break;
                case PLAYER_WALK_LEFT:
                    newAnimationState = PLAYER_IDLE_LEFT;
                    break;
                default:
                    newAnimationState = currentAnimationState;
                    break;
            }

            ChangeAnimationState(newAnimationState);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            FarmManager.instance.InteractWithTile(tf, new Vector3(0, -0.75f, 0));
        }
    }

    public void OnToggleDashboard(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            bool paused = GamePause.instance.TogglePause();
            if (paused) {
                playerControls.Player.Disable();
            }
            else {
                playerControls.Player.Enable();
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = playerMovement * movementSpeed;
        if (rb.velocity != Vector2.zero) {
            if (!StaminaSystem.instance.UseStamina(staminaConsumeRate)) {
                rb.velocity = Vector2.zero;
                tf.position = bedPosition;
                StaminaSystem.instance.RecoverFullStamina();
                Date.instance.AddDay();
            }
        }
    }

    // Change player animation states
    void ChangeAnimationState(string newAnimationState)
    {
        // Prevent animation from interrupting itself
        if (currentAnimationState == newAnimationState) return;

        // Play new animation
        animator.Play(newAnimationState);

        // Update current state
        currentAnimationState = newAnimationState;
    }
}

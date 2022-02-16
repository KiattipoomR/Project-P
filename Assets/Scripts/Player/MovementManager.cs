using UnityEngine;

public class MovementManager : MonoBehaviour
{
    // Components
    Rigidbody2D rb;

    // Player attributes
    float playerMoveSpeed = 5f;
    float playerMoveSpeedLimit = 0.7f;
    float inputHorizontal;
    float inputVertical;

    // Animations and states
    Animator animator;
    string currentAnimationState;

    const string PLAYER_IDLE_FRONT = "Player_Idle_Front";
    const string PLAYER_IDLE_BACK = "Player_Idle_Back";
    const string PLAYER_IDLE_RIGHT = "Player_Idle_Right";
    const string PLAYER_IDLE_LEFT = "Player_Idle_Left";
    const string PLAYER_WALK_UP = "Player_Walk_Up";
    const string PLAYER_WALK_DOWN = "Player_Walk_Down";
    const string PLAYER_WALK_RIGHT = "Player_Walk_Right";
    const string PLAYER_WALK_LEFT = "Player_Walk_Left";


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle inputs
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (inputHorizontal > 0)
        {
            ChangeAnimationState(PLAYER_WALK_RIGHT);
        }
        else if (inputHorizontal < 0)
        {
            ChangeAnimationState(PLAYER_WALK_LEFT);
        }
        else if (inputVertical > 0)
        {
            ChangeAnimationState(PLAYER_WALK_UP);
        }
        else if (inputVertical < 0)
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
                    newAnimationState = PLAYER_IDLE_FRONT;
                    break;
            }

            ChangeAnimationState(newAnimationState);
        }
    }

    void FixedUpdate()
    {
        // Resolve movements
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= playerMoveSpeedLimit;
                inputVertical *= playerMoveSpeedLimit;
            }
            rb.velocity = new Vector2(inputHorizontal * playerMoveSpeed, inputVertical * playerMoveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
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

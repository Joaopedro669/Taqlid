using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private InputSystem_Actions playerinput;
    private InputAction move;
    private InputAction jump;
    private InputAction interact;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 17;
    [SerializeField] private float jforce = 10;
     private Transform ground_pivot;
     [SerializeField] private LayerMask ground_layer;
    private Vector2 m_Velocity = Vector2.zero;
    [SerializeField] [Range(0.05f, 0.3f)]
    private float m_MovementSmoothing = 0.1f;
    private GameObject Hide;
    public bool IsHide = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerinput = new InputSystem_Actions();
    }
    //move
    void OnEnable()
    {
        move = playerinput.Player.Move;
        move.Enable();
        jump = playerinput.Player.Jump;
        jump.Enable();
        interact = playerinput.Player.Interact;
        interact.Enable();
    }
     void OnDisable()
    {
        move.Disable();
        jump.Disable();
        interact.Disable();
    }
     public float Speed
    {
        get
        {
            return speed;
        }
        set 
        {
            speed = value;
        }
    }
    private bool canMove = true;
    private bool canJump = true;
    public bool CanJump
    {
        get
        {
            return canJump;
        }
        set 
        {
            canJump = value;
        }
    }
    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground_pivot = transform.GetChild(0);
        Hide = GameObject.FindWithTag("HideSpot");
    }
    // Update is called once per frame
    void Update()
    {
        moveInput = move.ReadValue<Vector2>();

        if (jump.WasPressedThisFrame() == true 
            && detectGround() == true && canJump == true)
        {
            jump_action();
        }

    }
     private bool detectGround()
    {
        Collider2D col = Physics2D.OverlapCircle(
            ground_pivot.position,0.1f,ground_layer);
        if (col == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void jump_action()
    {
        rb.AddForce(transform.up * jforce, ForceMode2D.Impulse);
    }
    private void move_action()
    {

    }

    void FixedUpdate()
    {
        if (canMove == true )
        {
            Vector2 targetVelocity =
        new Vector2(moveInput.x * speed * 10 * Time.fixedDeltaTime,
           rb.linearVelocity.y);

           rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity,
           targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        
    }
    void Esconder()
    {
        
    }
}


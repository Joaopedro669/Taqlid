using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    private InputSystem_Actions playerinput;
    private InputAction move;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    private Vector2 m_Velocity = Vector2.zero;
    [SerializeField] [Range(0.05f, 0.03f)]
    private float m_MovementSmoothing = 0.01f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerinput = new InputSystem_Actions();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        move = playerinput.Player.Move;
        move.Enable();
    }
     void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = move.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        Vector2 targetVelocity =
        new Vector2(moveInput.x * speed * 10 * Time.fixedDeltaTime,
           moveInput.y * speed * 10 * Time.fixedDeltaTime);

           rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity,
           targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }
}


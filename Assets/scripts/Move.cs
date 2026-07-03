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

    [SerializeField] public float speed = 17;
    [SerializeField] private float jforce = 10;
    private Transform ground_pivot;
    [SerializeField] private LayerMask ground_layer;
    private Vector2 m_Velocity = Vector2.zero;
    [SerializeField] [Range(0.05f, 0.3f)] private float m_MovementSmoothing = 0.1f;
    private GameObject Hide;
    public bool IsHide = false;

    public bool ismoving = false;
    public AudioSource audio;

    // Variável para controlar a imunidade ao gás
    public bool IsImmuneToGas = false; 

    [Header("Configurações dos Lasers de Parede")]
    [SerializeField] private Transform wallDetectorHead;   // Arraste o objeto da Cabeça aqui
    [SerializeField] private Transform wallDetectorCenter; // Arraste o objeto do Centro aqui
    [SerializeField] private Transform wallDetectorFoot;   // Arraste o objeto do Pé aqui
    [SerializeField] private LayerMask blocoProtecaoLayer;  // Selecione a layer "ProtecaoGas"
    [SerializeField] private float laserLength = 0.5f;      // Comprimento curto, pois sai da borda/centro do player

    void Awake()
    {
        playerinput = new InputSystem_Actions();
    }

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

    public float Speed { get { return speed; } set { speed = value; } }
    private bool canMove = true;
    private bool canJump = true;
    public bool CanJump { get { return canJump; } set { canJump = value; } }
    public bool CanMove { get { return canMove; } set { canMove = value; } }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ground_pivot = transform.GetChild(0);
    }

    void Update()
    {
        moveInput = move.ReadValue<Vector2>();
        
        if (jump.WasPressedThisFrame() == true && detectGround() == true && canJump == true)
        {
            jump_action();
        }

        // Verifica se apertou E
        if (interact.WasPressedThisFrame())
        {
            if (IsImmuneToGas)
            {
                ToggleGasImmunity(false);
            }
            else if (DetectarBlocoProtecao())
            {
                ToggleGasImmunity(true);
            }
            else
            {
                Debug.Log("Você precisa estar encostado em um bloco de proteção!");
            }
        }

        // Se perder o contato com o bloco enquanto estiver imune, cancela o efeito automaticamente
        if (IsImmuneToGas && !DetectarBlocoProtecao())
        {
            ToggleGasImmunity(false);
        }

        if (moveInput.x != 0)
        {
            ismoving = true;
        }
        else
        {
            ismoving = false;
        }

        if (ismoving && audio.isPlaying == false && detectGround() == true)
        {
            audio.Play();
        }
        else if (ismoving == false || detectGround() == false)
        {
            audio.Stop();
        }
    }

    // Função que dispara os 3 pares de lasers (Cabeça, Centro e Pé)
    private bool DetectarBlocoProtecao()
    {
        // Garante que os componentes foram atribuídos no Inspector para evitar erros
        if (wallDetectorHead == null || wallDetectorCenter == null || wallDetectorFoot == null) return false;

        // Dispara raios para a Direita a partir dos 3 pontos
        RaycastHit2D hitHeadR = Physics2D.Raycast(wallDetectorHead.position, Vector2.right, laserLength, blocoProtecaoLayer);
        RaycastHit2D hitCenterR = Physics2D.Raycast(wallDetectorCenter.position, Vector2.right, laserLength, blocoProtecaoLayer);
        RaycastHit2D hitFootR = Physics2D.Raycast(wallDetectorFoot.position, Vector2.right, laserLength, blocoProtecaoLayer);

        // Dispara raios para a Esquerda a partir dos 3 pontos
        RaycastHit2D hitHeadL = Physics2D.Raycast(wallDetectorHead.position, Vector2.left, laserLength, blocoProtecaoLayer);
        RaycastHit2D hitCenterL = Physics2D.Raycast(wallDetectorCenter.position, Vector2.left, laserLength, blocoProtecaoLayer);
        RaycastHit2D hitFootL = Physics2D.Raycast(wallDetectorFoot.position, Vector2.left, laserLength, blocoProtecaoLayer);

        // Desenha os lasers visuais na aba Scene (Verde se bater no bloco, Vermelho se não bater)
        Debug.DrawRay(wallDetectorHead.position, Vector2.right * laserLength, hitHeadR.collider ? Color.green : Color.red);
        Debug.DrawRay(wallDetectorCenter.position, Vector2.right * laserLength, hitCenterR.collider ? Color.green : Color.red);
        Debug.DrawRay(wallDetectorFoot.position, Vector2.right * laserLength, hitFootR.collider ? Color.green : Color.red);
        
        Debug.DrawRay(wallDetectorHead.position, Vector2.left * laserLength, hitHeadL.collider ? Color.green : Color.red);
        Debug.DrawRay(wallDetectorCenter.position, Vector2.left * laserLength, hitCenterL.collider ? Color.green : Color.red);
        Debug.DrawRay(wallDetectorFoot.position, Vector2.left * laserLength, hitFootL.collider ? Color.green : Color.red);

        // Retorna verdadeiro se QUALQUER um dos 6 lasers encontrar o bloco correto
        return (hitHeadR.collider != null || hitCenterR.collider != null || hitFootR.collider != null ||
                hitHeadL.collider != null || hitCenterL.collider != null || hitFootL.collider != null);
    }

    void ToggleGasImmunity(bool ativar)
    {
        IsImmuneToGas = ativar;

        if (IsImmuneToGas)
        {
            canMove = false;
            canJump = false;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            Debug.Log("Imunidade ativada junto ao bloco protetor!");
        }
        else
        {
            canMove = true;
            canJump = true;
            Debug.Log("Imunidade desativada.");
        }
    }

    private bool detectGround()
    {
        Collider2D col = Physics2D.OverlapCircle(ground_pivot.position, 0.1f, ground_layer);
        return col != null;
    }

    private void jump_action()
    {
        rb.AddForce(transform.up * jforce, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            Vector2 targetVelocity = new Vector2(moveInput.x * speed * 10 * Time.fixedDeltaTime, rb.linearVelocity.y);
            rb.linearVelocity = Vector2.SmoothDamp(rb.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
    }

    void Esconder() { }
}




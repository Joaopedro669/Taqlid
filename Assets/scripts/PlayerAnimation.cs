using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Move moveScript;
    private Rigidbody2D rb;
    private bool viradoParaDireita = true;
    public Vector2 movedir;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<Move>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float velocidadeX = rb.linearVelocity.x;
        movedir = GetComponent<Move>().moveInput;
        /*
        if (Mathf.Abs(velocidadeX) < 0.1f)
        {
            velocidadeX = 0f;
        }*/
        Debug.Log(movedir.x);

        anim.SetFloat("direction_horizontal", velocidadeX);
        anim.SetBool("is_hiding", moveScript.IsImmuneToGas);
        anim.SetFloat("vertical_velocity", rb.linearVelocity.y);

        // Sistema de Giro (Flip)
        // Só gira se o personagem NÃO estiver escondido
        if (!moveScript.IsImmuneToGas)
        {
            if (movedir.x > 0f)
            {
                viradoParaDireita = true;
            }
            else if (movedir.x < 0f)
            {
                viradoParaDireita = false;
            }
            GirarObjeto();
        }
    }

    void GirarObjeto()
    {
        // Inverte a escala X do personagem para espelhar ele e todas as animações
        GetComponent<SpriteRenderer>().flipX = viradoParaDireita;
    }
}

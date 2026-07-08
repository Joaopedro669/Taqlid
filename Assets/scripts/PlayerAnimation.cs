using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Move moveScript;
    private Rigidbody2D rb;
    private Vector2 movedir;
    private bool viradoParaDireita = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveScript = GetComponent<Move>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float velocidadeX = rb.linearVelocity.x;
        movedir = moveScript.moveInput;

        if (Mathf.Abs(velocidadeX) < 0.1f)
        {
            velocidadeX = 0f;
        }

        // O Mathf.Abs transforma o -17 de andar para a esquerda em +17, ativando o andar padrão!
        anim.SetFloat("direction_horizontal", Mathf.Abs(velocidadeX));
        anim.SetBool("is_hiding", moveScript.IsImmuneToGas || moveScript.IsHide);
        anim.SetFloat("vertical_velocity", rb.linearVelocity.y);

        bool escondido = moveScript.IsImmuneToGas || moveScript.IsHide;

        if (!escondido)
        {
            if (movedir.x > 0.1f && !viradoParaDireita)
            {
                GirarObjeto();
            }
            else if (movedir.x < -0.1f && viradoParaDireita)
            {
                GirarObjeto();
            }
        }
    }

    void GirarObjeto()
    {
        viradoParaDireita = !viradoParaDireita;

        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}

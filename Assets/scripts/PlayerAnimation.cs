using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Move moveScript;
    private Rigidbody2D rb;
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

        if (Mathf.Abs(velocidadeX) < 0.1f)
        {
            velocidadeX = 0f;
        }

        anim.SetFloat("direction_horizontal", velocidadeX);
        anim.SetBool("is_hiding", moveScript.IsImmuneToGas);
        anim.SetFloat("vertical_velocity", rb.linearVelocity.y);

        // Sistema de Giro (Flip)
        // Só gira se o personagem NÃO estiver escondido
        if (!moveScript.IsImmuneToGas)
        {
            if (velocidadeX > 0.1f && !viradoParaDireita)
            {
                GirarObjeto();
            }
            else if (velocidadeX < -0.1f && viradoParaDireita)
            {
                GirarObjeto();
            }
        }
    }

    void GirarObjeto()
    {
        viradoParaDireita = !viradoParaDireita;

        // Inverte a escala X do personagem para espelhar ele e todas as animações
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}

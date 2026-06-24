using UnityEngine;

public class impulseforce : MonoBehaviour
{
    // Força do pulo que você pode ajustar pelo Inspetor do Unity
    [SerializeField] private float forcaImpulso = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem pisou na plataforma tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // Zera a velocidade vertical atual para o impulso ser sempre igual
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

                // Aplica a força para cima (usando Impulse para ser instantâneo)
                rb.AddForce(Vector2.up * forcaImpulso, ForceMode2D.Impulse);
            }
        }
    }
}

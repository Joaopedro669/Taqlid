using UnityEngine;

public class FallDetector : MonoBehaviour
{
    [SerializeField] private Color debugColor = Color.red;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se quem caiu no abismo foi o jogador
        if (collision.CompareTag("Player"))
        {
            HP jogadorHP = collision.GetComponent<HP>();

            if (jogadorHP != null)
            {
                // Tira apenas 1 de HP do jogador
                jogadorHP.perderHP(1);

                // Se o jogador NÃO morreu (ainda tem HP sobrando),
                // apenas teleporta ele de volta para o checkpoint
                if (jogadorHP.HPAtual > 0)
                {
                    jogadorHP.TeleportarParaCheckpoint();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        if (box != null)
        {
            Gizmos.color = debugColor;
            Vector3 size = new Vector3(box.size.x * transform.localScale.x, box.size.y * transform.localScale.y, 1);
            Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, size);
        }
    }
}


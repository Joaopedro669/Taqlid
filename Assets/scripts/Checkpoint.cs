using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    // Variável que salva se este checkpoint específico já foi ativado
    private bool jaAtivado = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o checkpoint já foi usado uma vez, ignora qualquer nova colisão
        if (jaAtivado) return;

        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player"))
        {
            // Pega o componente HP que está no jogador
            HP playerRespawn = collision.GetComponent<HP>();
            
            if (playerRespawn != null)
            {
                // Atualiza o ponto de respawn com a posição deste checkpoint
                playerRespawn.UpdateRespawnPoint(transform.position);

                // Bloqueia este checkpoint para sempre
                jaAtivado = true;
                Debug.Log("Checkpoint " + gameObject.name + " ativado e bloqueado!");
            }
        }
    }
}


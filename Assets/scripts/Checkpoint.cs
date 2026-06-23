using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (collision.CompareTag("Player"))
        {
            // Pega o componente PlayerRespawn que está no jogador
            HP playerRespawn = collision.GetComponent<HP>();
            
            if (playerRespawn != null)
            {
                // Atualiza o ponto de respawn com a posição deste checkpoint
                playerRespawn.UpdateRespawnPoint(transform.position);
            }
        }
    }
}

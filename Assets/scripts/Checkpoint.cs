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
                // Atualiza o ponto de respawn local (pra morte/respawn dentro da mesma cena)
                playerRespawn.UpdateRespawnPoint(transform.position);

                // Salva o checkpoint persistente (PlayerPrefs), usado pelo botão "Continuar"
                if (CheckpointManager.Instance != null)
                {
                    CheckpointManager.Instance.SaveCheckpoint(transform.position);
                }
                else
                {
                    Debug.LogWarning("CheckpointManager.Instance é nulo! O checkpoint não foi salvo de forma persistente.");
                }

                // Bloqueia este checkpoint para sempre
                jaAtivado = true;
                Debug.Log("Checkpoint " + gameObject.name + " ativado e bloqueado!");
            }
        }
    }
}
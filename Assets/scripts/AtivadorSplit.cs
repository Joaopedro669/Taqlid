using UnityEngine;

public class AtivadorSplit : MonoBehaviour
{
    // Executa automaticamente quando algo entra na área verde
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se quem entrou na área foi o Player
        if (other.CompareTag("Player"))
        {
            // Procura o Animator no Player e ativa o gatilho da animação
            Animator playerAnim = other.GetComponent<Animator>();
            if (playerAnim != null)
            {
                playerAnim.SetTrigger("ativar_split");
            }

            
        }
    }
}

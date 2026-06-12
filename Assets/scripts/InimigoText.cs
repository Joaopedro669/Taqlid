using UnityEngine;

public class InimigoTeste : MonoBehaviour
{
    // Executa automaticamente quando algo entra na área do gatilho
    private void OnTriggerEnter2D(Collider2D colisor)
    {
        // Verifica se o objeto que encostou tem a tag "Player"
        if (colisor.CompareTag("Player"))
        {
            // Busca o script de vidas no jogador e aplica 1 de dano
            HP scriptHP = colisor.GetComponent<HP>();
            
            if (scriptHP != null)
            {
                scriptHP.perderHP(1);
                Debug.Log("O jogador encostou no inimigo e perdeu 1 vida!");
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para mudar de fase

public class FinalizadorFase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CarregarProximaFase();
        }
    }

    private void CarregarProximaFase()
    {
        // Pega o índice da fase atual e soma 1 para ir para a próxima
        int proximaCena = SceneManager.GetActiveScene().buildIndex + 1;
        
        // Carrega a nova cena
        SceneManager.LoadScene(proximaCena);
    }
}

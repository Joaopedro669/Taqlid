using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGerenciador : MonoBehaviour
{
    // Guarda na memória global do jogo o nome da última cena visitada antes dos créditos
    private static string cenaAnterior = "Menu";

    // Função para abrir o Menu Principal direto (útil para a tela de Vitória/GameOver)
    public void VoltarAoMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    // Função para abrir os Créditos (Salva a cena atual na memória antes de mudar)
    public void AbrirCreditos()
    {
        cenaAnterior = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Creditos");
    }

    // Função para o botão Voltar da tela de Créditos (Lê a memória e devolve o jogador ao lugar certo)
    public void VoltarDosCreditos()
    {
        SceneManager.LoadScene(cenaAnterior);
    }

    // Função para fechar o jogo
    public void SairDoJogo()
    {
        Debug.Log("O jogador clicou em Sair!");
        Application.Quit(); // Fecha o jogo compilado
    }
}

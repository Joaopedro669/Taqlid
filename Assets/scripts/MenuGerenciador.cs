using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGerenciador : MonoBehaviour
{
    private static string cenaAnterior = "Menu";

    // NOVO: Campo para arrastarmos a janelinha de confirmação direto pelo Inspector
    [Header("Janela de Confirmação de Saída")]
    [SerializeField] private GameObject painelConfirmacaoSair;

    public void VoltarAoMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }

    public void AbrirCreditos()
    {
        cenaAnterior = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Creditos");
    }

    public void VoltarDosCreditos()
    {
        SceneManager.LoadScene(cenaAnterior);
    }

    // MODIFICADO: Agora o botão de Sair apenas ABRE a janelinha de confirmação na tela
    public void ClicouEmSair()
    {
        if (painelConfirmacaoSair != null)
        {
            painelConfirmacaoSair.SetActive(true);
        }
        else
        {
            // Se você esquecer de criar o painel em alguma cena, o jogo fecha direto por segurança
            ConfirmouSairSim();
        }
    }

    // NOVO: Função para o botão NÃO (apenas fecha a janelinha e volta para o menu)
    public void ConfirmouSairNao()
    {
        if (painelConfirmacaoSair != null)
        {
            painelConfirmacaoSair.SetActive(false);
        }
    }

    // NOVO: Função para o botão SIM (fecha o jogo de verdade)
    public void ConfirmouSairSim()
    {
        Debug.Log("O jogador confirmou e o jogo FECHOU!");
        Application.Quit(); // Fecha o jogo compilado
    }
}

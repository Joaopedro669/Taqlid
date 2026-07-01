using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Referências de UI")]
    [SerializeField] private GameObject painelPause;   // Painel/Canvas com os botões de pausa (começa desativado)
    [SerializeField] private Button pauseButton;        // Botão visível durante o jogo, que ABRE a pausa
    [SerializeField] private Button continueButton;     // Botão dentro do painel: "Continuar" (destrava)
    [SerializeField] private Button menuButton;          // Botão dentro do painel: "Voltar ao Menu"

    [Header("Configuração")]
    [SerializeField] private string nomeCenaMenu = "Menu";

    private bool jogoPausado = false;

    void Start()
    {
        // Garante que o painel comece fechado, mesmo se deixar ativo no Inspector por engano
        if (painelPause != null)
            painelPause.SetActive(false);

        if (pauseButton != null)
            pauseButton.onClick.AddListener(PausarJogo);

        if (continueButton != null)
            continueButton.onClick.AddListener(ContinuarJogo);

        if (menuButton != null)
            menuButton.onClick.AddListener(VoltarAoMenu);
    }

    public void PausarJogo()
    {
        jogoPausado = true;
        Time.timeScale = 0f;

        if (painelPause != null)
            painelPause.SetActive(true);

        // Esconde o botão de pause enquanto o painel estiver aberto
        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);
    }

    public void ContinuarJogo()
    {
        jogoPausado = false;
        Time.timeScale = 1f;

        if (painelPause != null)
            painelPause.SetActive(false);

        // Reativa o botão de pause ao voltar para o jogo
        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }

    public void VoltarAoMenu()
    {
        // IMPORTANTE: restaura o timeScale ANTES de trocar de cena,
        // senão a cena do Menu carregaria travada também.
        Time.timeScale = 1f;
        jogoPausado = false;

        SceneManager.LoadScene(nomeCenaMenu);
    }

    void OnDisable()
    {
        // Segurança extra: se esse objeto for desativado/destruído
        // enquanto o jogo estava pausado, evita que o timeScale fique em 0 pra sempre
        // e garante que o botão de pause não suma para sempre.
        Time.timeScale = 1f;

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }
}
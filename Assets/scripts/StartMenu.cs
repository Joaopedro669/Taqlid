using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;    // "Novo Jogo"
    [SerializeField] private Button continueButton; // "Continuar"
    [SerializeField] private Button configButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button returnButton;

    void Start()
    {
        startButton.onClick.AddListener(NewGame);
        continueButton.onClick.AddListener(ContinueGame);
        configButton.onClick.AddListener(Config);
        creditsButton.onClick.AddListener(Credits);

        // COMENTADO: Desativado para o botão Sair não fechar o jogo direto antes da hora
        // exitButton.onClick.AddListener(ExitGame);

        // COMENTADO: Desativado para sumir com o erro vermelho do botão que não existe no seu menu
        // returnButton.onClick.AddListener(Return);

        // Desativa "Continuar" se não houver save
        bool hasSave = PlayerPrefs.GetInt("checkpoint_exists", 0) == 1;
        continueButton.interactable = hasSave;
    }

    public void NewGame()
    {
        // Apaga o checkpoint e começa do zero
        CheckpointManager.ClearCheckpoint();
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Config()
    {
        SceneManager.LoadScene("Config");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void ExitGame()
    {
        Debug.Log("O jogador fechou o jogo!");
        Application.Quit();
    }

    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
}

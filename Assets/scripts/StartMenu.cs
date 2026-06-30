using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        exitButton.onClick.AddListener(ExitGame);
        returnButton.onClick.AddListener(Return);

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
        string savedScene = PlayerPrefs.GetString("checkpoint_scene", "SampleScene");
        SceneManager.LoadScene(savedScene);
        // O CheckpointManager.Start() reposiciona o jogador automaticamente
    }

    public void Config()
    {
        SceneManager.LoadScene("Config");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }
    
    public void Return()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}

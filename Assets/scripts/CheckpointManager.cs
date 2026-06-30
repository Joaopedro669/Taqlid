using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    private const string KEY_X     = "checkpoint_x";
    private const string KEY_Y     = "checkpoint_y";
    private const string KEY_SCENE = "checkpoint_scene";
    private const string KEY_EXISTS = "checkpoint_exists";

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Assim que a cena carrega, aplica o checkpoint salvo ao jogador
        ApplyCheckpointToPlayer();
    }

    // Chamado pelo CheckpointSingle ao tocar o checkpoint
    public void SaveCheckpoint(Vector2 position)
    {
        PlayerPrefs.SetFloat(KEY_X, position.x);
        PlayerPrefs.SetFloat(KEY_Y, position.y);
        PlayerPrefs.SetString(KEY_SCENE, SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(KEY_EXISTS, 1);
        PlayerPrefs.Save();
        Debug.Log($"Checkpoint salvo: {position} na cena {SceneManager.GetActiveScene().name}");
    }

    // Verifica se existe algum checkpoint salvo
    public bool HasSavedCheckpoint()
    {
        return PlayerPrefs.GetInt(KEY_EXISTS, 0) == 1;
    }

    // Retorna a cena salva
    public string GetSavedScene()
    {
        return PlayerPrefs.GetString(KEY_SCENE, "SampleScene");
    }

    // Após carregar a cena, aplica a posição ao jogador
    public void ApplyCheckpointToPlayer()
    {
        if (!HasSavedCheckpoint()) return;

        // Só aplica se estiver na cena corretas
        if (SceneManager.GetActiveScene().name != GetSavedScene()) return;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null) return;

        Vector2 savedPos = new Vector2(
            PlayerPrefs.GetFloat(KEY_X, 0f),
            PlayerPrefs.GetFloat(KEY_Y, 0f)
        );

        playerObj.transform.position = savedPos;

        HP hp = playerObj.GetComponent<HP>();
        if (hp != null)
            hp.UpdateRespawnPoint(savedPos);

        Debug.Log($"Checkpoint aplicado: {savedPos}");
    }

    // Apaga o save (para o botão "Novo Jogo")
    public static void ClearCheckpoint()
    {
        PlayerPrefs.DeleteKey("checkpoint_x");
        PlayerPrefs.DeleteKey("checkpoint_y");
        PlayerPrefs.DeleteKey("checkpoint_scene");
        PlayerPrefs.DeleteKey("checkpoint_exists");
        PlayerPrefs.Save();
    }
}
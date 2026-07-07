using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // NOVO: Linha adicionada para a Unity aceitar imagens na UI

public class HP : MonoBehaviour
{
    private Vector2 respawnPoint;
    private Rigidbody2D rb;
    private bool estaMorto = false;

    [Header("Configurações de HP")]
    public int HPTotal = 4;
    public int HPAtual;

    [Header("Configurações de Vidas Extras")]
    private int vidasExtrasAtuais;

    [Header("Componentes de UI")]
    public TextMeshProUGUI textoHP;
    public TextMeshProUGUI textoVidas;
    public Image barraPreenchimentoHP; // NOVO: Caixinha onde vamos arrastar a barra vermelha

    void Start()
    {
        HPAtual = HPTotal;
        vidasExtrasAtuais = 2; // Mantém a correção das 3 tentativas totais
        estaMorto = false;

        AtualizarUI();

        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (HPAtual <= 0)
        {
            Morrer();
        }
    }
    public void UpdateRespawnPoint(Vector2 newPosition)
    {
        respawnPoint = newPosition;
    }

    public void perderHP(int QuantidadedeDano)
    {
        if (estaMorto) return;

        HPAtual = Mathf.Max(0, HPAtual - QuantidadedeDano);
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (textoHP != null)
        {
            textoHP.text = "HP Restante: " + HPAtual;
        }

        if (textoVidas != null)
        {
            textoVidas.text = "Vidas Extras: " + vidasExtrasAtuais;
        }

        // NOVO: Faz a barra vermelha encolher baseada na conta (Vida Atual dividida pela Vida Máxima)
        if (barraPreenchimentoHP != null)
        {
            barraPreenchimentoHP.fillAmount = (float)HPAtual / HPTotal;
        }
    }

    void Morrer()
    {
        estaMorto = true;
        Debug.Log("O jogador perdeu todo o HP!");

        if (vidasExtrasAtuais <= 0)
        {
            GameOver();
        }
        else
        {
            vidasExtrasAtuais--;
            AtualizarUI();
            Respawn();
        }
    }

    public void Respawn()
    {
        TeleportarParaCheckpoint();

        // Restaura o HP completo porque ele gastou uma vida extra
        HPAtual = HPTotal;
        AtualizarUI();

        estaMorto = false;
    }

    public void TeleportarParaCheckpoint()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
        transform.position = respawnPoint;
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Derrota");
    }
}

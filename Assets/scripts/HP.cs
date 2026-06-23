using UnityEngine;
using TMPro; // Importa o TextMeshPro

public class HP : MonoBehaviour 
{
    private Vector2 respawnPoint;
    private Rigidbody2D rb;
    private bool estaMorto = false; // Evita que o jogador morra duas vezes seguidas no mesmo frame

    [Header("Configurações de HP")]
    public int HPTotal = 3;
    public int HPAtual;

    [Header("Configurações de Vidas Extras")]
    public int vidasExtrasTotais = 3; // Começa com 3 vidas extras
    private int vidasExtrasAtuais;

    [Header("Componentes de UI")]
    public TextMeshProUGUI textoHP;
    public TextMeshProUGUI textoVidas; // Arraste o texto das vidas aqui no Unity

void Start() 
{
    HPAtual = HPTotal;
    
    // Força o jogo a ter exatamente 2 vidas extras na reserva.
    // Assim: Vida Inicial (1) + Vidas Extras (2) = 3 tentativas no total!
    vidasExtrasAtuais = 2; 
    
    estaMorto = false;
    
    AtualizarUI();
    
    respawnPoint = transform.position;
    rb = GetComponent<Rigidbody2D>();
}


    public void UpdateRespawnPoint(Vector2 newPosition) 
    {
        respawnPoint = newPosition;
    }

    public void perderHP(int QuantidadedeDano) 
    {
        // Se o jogador já está no processo de morte, ignora novos danos
        if (estaMorto) return;

        // Reduz o HP e impede que ele fique negativo
        HPAtual = Mathf.Max(0, HPAtual - QuantidadedeDano);
        AtualizarUI();

        // Se o HP zerar, chama a lógica de morte
        if (HPAtual <= 0) 
        {
            Morrer();
        }
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
    }

    void Morrer() 
    {
        estaMorto = true; // Bloqueia outras chamadas de dano/morte imediatas
        Debug.Log("O jogador perdeu todo o HP!");

        // Se ele não tem mais vidas extras (está em 0), Game Over direto sem tirar nada
        if (vidasExtrasAtuais <= 0) 
        {
            GameOver();
        } 
        // Se ele tem vidas extras (3, 2 ou 1), ele gasta uma e renasce
        else 
        {
            vidasExtrasAtuais--; // Perde 1 vida extra
            AtualizarUI();
            Respawn();
        }
    }

    public void Respawn() 
    {
        Debug.Log("Executando Respawn");
        
        if (rb != null) 
        {
            rb.linearVelocity = Vector2.zero;
        }
        
        transform.position = respawnPoint;
        
        // Restaura o HP para o valor total
        HPAtual = HPTotal; 
        AtualizarUI();
        
        // Libera o jogador para tomar dano novamente
        estaMorto = false; 
    }

    void GameOver()
    {
        Debug.Log("Game Over! O jogador perdeu todas as vidas extras e não irá mais respawnar.");
        
        // Desativa o jogador na tela para ele sumir em vez de respawnar
        gameObject.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Se cair no abismo, aciona a perda de HP direto
        if (collision.CompareTag("FallDetector")) 
        {
            perderHP(HPAtual); 
        }
    }
}

using UnityEngine;
using TMPro; // Importa o TextMeshPro

public class HP : MonoBehaviour 
{ 
    private Vector2 respawnPoint;
    private GameObject player;
    private Rigidbody2D rb;
    public HP var;
    public int HPTotal = 3; 
    public int HPAtual; 
    
    // Substitui o Text antigo pelo componente do TextMeshPro
    public TextMeshProUGUI textoHP; 

    void Start() 
    { 
        HPAtual = HPTotal; 
        AtualizarUI(); 
        // Define o ponto inicial da fase como o primeiro checkpoint
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        var = GetComponent<HP>();
    } 
    void FixedUpdate()
    {
        if(HPAtual <= 0)
        {
            
            Respawn();
            HPAtual = var.HPTotal;
        }      
    }
    public void UpdateRespawnPoint(Vector2 newPosition)
    {
        respawnPoint = newPosition;
    }
    public void perderHP (int QuantidadedeDano)
    {
        HPAtual -= QuantidadedeDano;
        AtualizarUI();

        // Se a vida zerar, ele morre. Se n�o zerar, ele N�O faz nada e fica no lugar!
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
    } 

    void Morrer() 
    { 
        Debug.Log("O jogador morreu!"); 
        
        
        // LINHA NOVA: Para qualquer dano contínuo do gás na hora da morte
        //StopAllCoroutines();
    } 
    // Chamado quando o jogador morre ou cai em um abismo
    public void Respawn()
    {
        Debug.Log("Morreu");
        // Zera a velocidade para n�o voltar 'voando'
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // Teleporta o jogador para o Ultimo checkpoint
        transform.position = respawnPoint;
        Debug.Log("Morreu a");

        // Se voc� estiver usando Cinemachine, pode precisar resetar a c�mera aqui para n�o dar 'teleporte visual'
    }

    // Exemplo: morre ao cair em um abismo (Trigger com tag "FallDetector")
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            Respawn();
        }
    }
}



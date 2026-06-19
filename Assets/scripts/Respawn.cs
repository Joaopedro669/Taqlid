using UnityEngine;
public class PlayerRespawn : HP
{
    private Vector2 respawnPoint;
    private GameObject player;
    private Rigidbody2D rb;
    public HP var;
    void Start()
    {
        // Define o ponto inicial da fase como o primeiro checkpoint
        player = GameObject.FindWithTag("Player");
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        var = GetComponent<HP>();
    }
    void FixedUpdate()
    {
        if(var.HPAtual <= 0)
        {
            
            Respawn();
            var.HPAtual = var.HPTotal;
        }      
    }

    // Chamado quando o jogador bate em um Checkpoint
    public void UpdateRespawnPoint(Vector2 newPosition)
    {
        respawnPoint = newPosition;
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

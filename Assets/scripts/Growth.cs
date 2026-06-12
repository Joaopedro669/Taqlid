using UnityEngine;
using UnityEngine.InputSystem; // Obrigatório para usar as novas ações de input

public class CrescerEParar : MonoBehaviour
{
    [Header("Configurações de Crescimento")]
    [SerializeField] private float velocidadeCrescimento = 1f;
    [SerializeField] private float tamanhoMaximo = 5f;

    [Header("Configurações de Pausa")]
    [SerializeField] private float tempoDePausa = 3f; // Tempo em segundos que ele fica sem crescer

    private Vector3 tamanhoOriginal;
    private float cronometroPausa = 0f;
    private bool podeCrescer = true;

    // Referência para o mapa de input gerado pela Unity (ajuste o nome se a sua classe for diferente)
    // Se você usa o PlayerInput por componente, pode obter a ação via string ou referência direta.
    [Header("Input")]
    private InputSystem_Actions playerInput; 
    private InputAction Clone;

    void Awake()
    {
        // Salva o tamanho inicial do objeto ao iniciar o jogo
        tamanhoOriginal = transform.localScale;
        playerInput = new InputSystem_Actions();
    }

    void OnEnable()
    {
        Clone = playerInput.Player.Clone;
        Clone.Enable();
    }

    void OnDisable()
    {
        Clone.Disable();
    }

    void Update()
    {
        GerenciarTemporizador();

        if (podeCrescer)
        {
            ExecutarCrescimento();
        }
    }

    private void ExecutarCrescimento()
    {
        // Verifica se o tamanho no eixo X ainda é menor que o tamanho máximo
        if (transform.localScale.x < tamanhoMaximo)
        {
            Vector3 crescimento = Vector3.one * velocidadeCrescimento * Time.deltaTime;
            transform.localScale += crescimento;

            // Garante que o objeto não passe do limite máximo por causa do arredondamento
            if (transform.localScale.x > tamanhoMaximo)
            {
                transform.localScale = Vector3.one * tamanhoMaximo;
            }
        }
    }

    private void GerenciarTemporizador()
    {
        if (!podeCrescer)
        {
            // Diminui o tempo restante da pausa baseado no tempo real decorrido
            cronometroPausa -= Time.deltaTime;

            if (cronometroPausa <= 0f)
            {
                podeCrescer = true;
            }
        }
    }

    // Função chamada automaticamente quando a ação Player.actions.Clone é executada
    private void RedefinirEPaudarObjeto(InputAction.CallbackContext context)
    {
        // Volta instantaneamente para o tamanho do início do jogo
        transform.localScale = tamanhoOriginal;

        // Ativa o bloqueio de crescimento e define o tempo de espera
        podeCrescer = false;
        cronometroPausa = tempoDePausa;
    }
}

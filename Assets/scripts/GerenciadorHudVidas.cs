using UnityEngine;
using System.Reflection; // Permite espiar variáveis privadas de forma segura

public class GerenciadorHudVidas : MonoBehaviour
{
    private HP playerHP;
    private FieldInfo campoVidasPrivado;

    [Header("Arraste os 2 ícones de vida extra aqui")]
    public GameObject iconeVidaExtra1;
    public GameObject iconeVidaExtra2;

    void Start()
    {
        // Encontra o componente HP no personagem Taqlid
        playerHP = GameObject.FindWithTag("Player").GetComponent<HP>();

        if (playerHP != null)
        {
            // Usamos reflexão para acessar a variável secreta/privada 'vidasExtrasAtuais' sem quebrar o script original
            campoVidasPrivado = typeof(HP).GetField("vidasExtrasAtuais", BindingFlags.NonPublic | BindingFlags.Instance);
        }
    }

    void Update()
    {
        if (playerHP == null || campoVidasPrivado == null) return;

        // Pega o valor real numérico direto de dentro do script HP (0, 1 ou 2)
        int vidasReais = (int)campoVidasPrivado.GetValue(playerHP);

        // Desliga ou liga as imagens baseado no número exato
        if (vidasReais == 2)
        {
            iconeVidaExtra1.SetActive(true);
            iconeVidaExtra2.SetActive(true);
        }
        else if (vidasReais == 1)
        {
            iconeVidaExtra1.SetActive(true);
            iconeVidaExtra2.SetActive(false); // Apaga a primeira vida extra gasta
        }
        else if (vidasReais <= 0)
        {
            iconeVidaExtra1.SetActive(false); // Apaga a segunda vida extra gasta
            iconeVidaExtra2.SetActive(false); // Fica com 0 ícones na tela
        }
    }
}

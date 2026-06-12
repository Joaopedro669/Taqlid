using UnityEngine;
using UnityEngine.UI;
public class HP : MonoBehaviour
{
    public int HPTotal = 3;
    private int HPAtual;

    public Text textoHP; 

    void Start()
    {
        HPAtual = HPTotal;
        AtualizarUI();
    }

    public void perderHP (int QuantidadedeDano)
    {
        HPAtual -= QuantidadedeDano;
        AtualizarUI();

        // Se a vida zerar, ele morre. Se não zerar, ele NÃO faz nada e fica no lugar!
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
        gameObject.SetActive(false); 
    }
}


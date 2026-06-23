using UnityEngine;
using TMPro; // Importa o TextMeshPro

public class HP : MonoBehaviour 
{ 
    public int HPTotal = 3; 
    public int HPAtual; 
    
    // Substitui o Text antigo pelo componente do TextMeshPro
    public TextMeshProUGUI textoHP; 

    void Start() 
    { 
        HPAtual = HPTotal; 
        AtualizarUI(); 
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
        gameObject.SetActive(false);
        
        // LINHA NOVA: Para qualquer dano contínuo do gás na hora da morte
        //StopAllCoroutines();
    } 
}



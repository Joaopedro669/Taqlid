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



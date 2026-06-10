using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private int GasTime = 0;
    [SerializeField] private int MaxGasTime = 300;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Começou");
    }

    // Update is called once per frame
    void Update()
    {
        Gas();
    }
    void Gas()
    {
        GasTime +=1;
        if(GasTime == MaxGasTime*10)
        {
            Debug.Log("Gas liberado");
            GasTime = 0;
            MaxGasTime = MaxGasTime - 30;
        }
    }
}

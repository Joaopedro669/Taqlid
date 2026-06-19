using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool GasTimerOn = true;
    [SerializeField] private int GasTime = 0;
    [SerializeField] private int MaxGasTime = 300;
    [SerializeField] private int GasReduction = 30;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Começou");
    }

    // Update is called once per frame
    void Update()
    {
        if(GasTimerOn == true )
        {
            Gas();
        }
        
    }
    void Gas()
    {
        GasTime +=1;
        if  (GasTime == MaxGasTime*10)
        {
            Debug.Log("Gas liberado");
            GasTime = 0;
            MaxGasTime = MaxGasTime - GasReduction;
        }
    }
}

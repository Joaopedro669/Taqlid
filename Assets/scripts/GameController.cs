using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool GasTimerOn = true;
    [SerializeField] private float GasTime = 0f;
    [SerializeField] private float InGasTime = 0f;
    [SerializeField] private float MaxGasTime = 5f;
    [SerializeField] private float GasDuration = 30f;
    [SerializeField] private float DamagePeriod = 10f;
    [SerializeField] private float InicialDamagePeriod = 0f;
    
    private GameObject player;
    // Cada 300 frames aqui são aprox. 1 segundo
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Começou");
        InicialDamagePeriod = DamagePeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if(GasTimerOn == true )
        {
            GasTime += Time.deltaTime;
            GasTimer();
        }
        else
        {
            InGasTime += Time.deltaTime;
            GasTimer();
            if(InGasTime >= DamagePeriod)
            {
                Debug.Log("Passou " + InicialDamagePeriod + " Segundos...........................");
                DamagePeriod += InicialDamagePeriod;
            }
        }
        
    }
    void GasTimer()
    {
       if(GasTime >= MaxGasTime)
       {
           Gas();
       }
       if (InGasTime >= GasDuration)
       {
           GasTime =0f;
           GasTimerOn = true;
           InGasTime = 0f;
           DamagePeriod = InicialDamagePeriod;
       }
    }
    void Gas()
    {
        GasTimerOn = false;
        
    }
}

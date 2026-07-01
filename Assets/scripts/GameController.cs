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

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Começou");
        InicialDamagePeriod = DamagePeriod;
    }

    void Update()
    {
        if (GasTimerOn == true)
        {
            GasTime += Time.deltaTime;
            GasTimer();
        }
        else
        {
            InGasTime += Time.deltaTime;
            GasTimer();

            if (InGasTime >= DamagePeriod)
            {
                Debug.Log("Passou " + InicialDamagePeriod + " Segundos...........................");
                
                // PEGA A REFERÊNCIA DO SCRIPT MOVE
                Move moveScript = player.GetComponent<Move>();

                // ALTERAÇÃO AQUI: Só toma dano se não estiver escondido E não estiver imune ao gás!
                if (moveScript.IsHide == false && moveScript.IsImmuneToGas == false)
                {
                    player.GetComponent<HP>().perderHP(1);
                    Debug.Log("Player levou 1 de dano do gás");
                }
                else if (moveScript.IsImmuneToGas)
                {
                    Debug.Log("Player está imune ao gás!");
                }

                DamagePeriod += InicialDamagePeriod;
            }
        }
    }

    void GasTimer()
    {
        if (GasTime >= MaxGasTime)
        {
            Gas();
        }
        if (InGasTime >= GasDuration)
        {
            GasTime = 0f;
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




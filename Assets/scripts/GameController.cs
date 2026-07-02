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

    // NOVO: Referência para o Particle System
    [SerializeField] private ParticleSystem gasParticles;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Começou");
        InicialDamagePeriod = DamagePeriod;

        // Opcional: Garante que o efeito comece desligado
        if (gasParticles != null)
        {
            gasParticles.Stop();
        }
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
                Move moveScript = player.GetComponent<Move>();
                
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
        
        // Modificado: Quando o tempo do gás acaba
        if (InGasTime >= GasDuration)
        {
            GasTime = 0f;
            GasTimerOn = true;
            InGasTime = 0f;
            DamagePeriod = InicialDamagePeriod;

            // Desativa partículas quando o gás some
            if (gasParticles != null)
            {
                gasParticles.Stop();
            }
        }
    }

    void Gas()
    {
        // Ativar as partículas apenas no momento exato em que o gás liga
        if (GasTimerOn == true && gasParticles != null)
        {
            gasParticles.Play();
        }

        GasTimerOn = false;
    }
}

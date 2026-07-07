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

    // REFERÊNCIA ATUALIZADA: Agora controlamos o objeto do gás por inteiro
    [SerializeField] private ParticleSystem gasParticles;

    [Header("Configurações de Áudio do Gás")]
    [SerializeField] private AudioSource somSirene;
    [SerializeField] private AudioSource somGas;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log("Começou");
        InicialDamagePeriod = DamagePeriod;

        // CORREÇÃO: Em vez de usar .Stop(), desligamos o objeto visual para garantir que suma no início
        if (gasParticles != null)
        {
            gasParticles.gameObject.SetActive(false);
        }

        if (somSirene != null) somSirene.Stop();
        if (somGas != null) somGas.Stop();
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

        if (InGasTime >= GasDuration)
        {
            GasTime = 0f;
            GasTimerOn = true;
            InGasTime = 0f;
            DamagePeriod = InicialDamagePeriod;

            // CORREÇÃO: Desativa o objeto inteiro do gás quando o tempo acaba
            if (gasParticles != null)
            {
                gasParticles.gameObject.SetActive(false);
            }

            if (somGas != null)
            {
                somGas.Stop();
            }
        }
    }

    void Gas()
    {
        if (GasTimerOn == true && gasParticles != null)
        {
            // CORREÇÃO: Liga o objeto do gás primeiro, e depois dá o Play nas partículas
            gasParticles.gameObject.SetActive(true);
            gasParticles.Play();

            if (somSirene != null) somSirene.Play();
            if (somGas != null) somGas.Play();
        }

        GasTimerOn = false;
    }
}

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ConfigMenu : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;
    // Nomes dos parâmetros expostos no AudioMixer (precisam bater EXATAMENTE)
    [SerializeField] private string paramVolumeGeral = "MasterVolume";
    [SerializeField] private string paramVolumeMusica = "MusicVolume";
    [SerializeField] private string paramVolumeSFX = "SFXVolume";

    [Header("Sliders")]
    [SerializeField] private Slider sliderVolumeGeral;
    [SerializeField] private Slider sliderVolumeMusica;
    [SerializeField] private Slider sliderVolumeSFX;

    [Header("Tela Cheia")]
    [SerializeField] private Button fullscreenButton;
    [SerializeField] private TextMeshProUGUI textoFullscreenButton;

    [Header("Voltar")]
    [SerializeField] private Button returnButton;
    [SerializeField] private string nomeCenaMenu = "Menu";

    // Chaves de save
    private const string KEY_VOL_GERAL = "volume_geral";
    private const string KEY_VOL_MUSICA = "volume_musica";
    private const string KEY_VOL_SFX = "volume_sfx";
    private const string KEY_FULLSCREEN = "tela_cheia";

    void Start()
    {
        CarregarConfiguracoes();

        if (sliderVolumeGeral != null)
            sliderVolumeGeral.onValueChanged.AddListener(SetVolumeGeral);

        if (sliderVolumeMusica != null)
            sliderVolumeMusica.onValueChanged.AddListener(SetVolumeMusica);

        if (sliderVolumeSFX != null)
            sliderVolumeSFX.onValueChanged.AddListener(SetVolumeSFX);

        if (fullscreenButton != null)
            fullscreenButton.onClick.AddListener(AlternarTelaCheia);

        if (returnButton != null)
            returnButton.onClick.AddListener(VoltarAoMenu);
    }

    // ---------- VOLUME ----------

    public void SetVolumeGeral(float valor)
    {
        AplicarVolume(paramVolumeGeral, valor);
        PlayerPrefs.SetFloat(KEY_VOL_GERAL, valor);
        PlayerPrefs.Save();
    }

    public void SetVolumeMusica(float valor)
    {
        AplicarVolume(paramVolumeMusica, valor);
        PlayerPrefs.SetFloat(KEY_VOL_MUSICA, valor);
        PlayerPrefs.Save();
    }

    public void SetVolumeSFX(float valor)
    {
        AplicarVolume(paramVolumeSFX, valor);
        PlayerPrefs.SetFloat(KEY_VOL_SFX, valor);
        PlayerPrefs.Save();
    }

    // Converte o valor linear do slider (0.0001 a 1) para decibéis (-80 a 0),
    // que é a escala que o AudioMixer usa de verdade.
    private void AplicarVolume(string parametro, float valorSlider)
    {
        if (audioMixer == null) return;

        float valorDb = (valorSlider <= 0.0001f)
            ? -80f
            : Mathf.Log10(valorSlider) * 20f;

        audioMixer.SetFloat(parametro, valorDb);
    }

    // ---------- TELA CHEIA ----------

    public void AlternarTelaCheia()
    {
        bool novoEstado = !Screen.fullScreen;
        Screen.fullScreen = novoEstado;

        AtualizarTextoFullscreen(novoEstado);

        PlayerPrefs.SetInt(KEY_FULLSCREEN, novoEstado ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void AtualizarTextoFullscreen(bool telaCheia)
    {
        if (textoFullscreenButton == null) return;

        textoFullscreenButton.text = telaCheia ? "Modo Janela" : "Tela Cheia";
    }

    // ---------- VOLTAR ----------

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }

    // ---------- CARREGAR CONFIGURAÇÕES SALVAS ----------

    private void CarregarConfiguracoes()
    {
        float volGeral = PlayerPrefs.GetFloat(KEY_VOL_GERAL, 1f);
        float volMusica = PlayerPrefs.GetFloat(KEY_VOL_MUSICA, 1f);
        float volSFX = PlayerPrefs.GetFloat(KEY_VOL_SFX, 1f);
        bool telaCheia = PlayerPrefs.GetInt(KEY_FULLSCREEN, Screen.fullScreen ? 1 : 0) == 1;

        // Atualiza os sliders visualmente (sem disparar o listener antes da hora)
        if (sliderVolumeGeral != null) sliderVolumeGeral.SetValueWithoutNotify(volGeral);
        if (sliderVolumeMusica != null) sliderVolumeMusica.SetValueWithoutNotify(volMusica);
        if (sliderVolumeSFX != null) sliderVolumeSFX.SetValueWithoutNotify(volSFX);

        // Aplica de fato no AudioMixer
        AplicarVolume(paramVolumeGeral, volGeral);
        AplicarVolume(paramVolumeMusica, volMusica);
        AplicarVolume(paramVolumeSFX, volSFX);

        // Aplica tela cheia salva
        Screen.fullScreen = telaCheia;
        AtualizarTextoFullscreen(telaCheia);
    }
}
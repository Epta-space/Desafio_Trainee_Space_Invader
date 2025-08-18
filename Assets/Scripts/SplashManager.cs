using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashManager : MonoBehaviour
{
    // Variável estática para controlar se o splash já foi exibido nesta sessão
    private static bool hasBeenShown = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject splashScreenObject;
    
    [SerializeField]
    [Range(0f, 1f)]
    private float initialVolume = 0.5f; // Volume inicial do vídeo (0 = mudo, 1 = volume máximo)
    
    [SerializeField]
    [Tooltip("Desabilita o splash screen no editor para facilitar desenvolvimento")]
    private bool disableInEditor = true;
    
    [SerializeField]
    [Tooltip("Pausa o jogo enquanto o splash está sendo exibido")]
    private bool pauseGameDuringSplash = true;
    
    [SerializeField]
    [Tooltip("Exibe o splash apenas uma vez por sessão do jogo")]
    private bool showOnlyOnce = true;
    
    private VideoPlayer videoPlayer;
    private float originalTimeScale;
    private List<AudioSource> pausedAudioSources = new List<AudioSource>();
    void Start()
    {
        // Verifica se deve pular o splash screen no editor
        if (Application.isEditor && disableInEditor)
        {
            Debug.Log("Splash screen desabilitado no Editor para facilitar desenvolvimento");
            return;
        }

        // Verifica se o splash já foi exibido nesta sessão
        if (showOnlyOnce && hasBeenShown)
        {
            Debug.Log("Splash screen já foi exibido nesta sessão, pulando...");
            return;
        }

        // Salva o TimeScale original
        originalTimeScale = Time.timeScale;

        #if !UNITY_EDITOR
                // Encontra o VideoPlayer no objeto filho "Eptinho Video"
                if (splashScreenObject != null)
                {
                    videoPlayer = splashScreenObject.GetComponentInChildren<VideoPlayer>();
                    if (videoPlayer != null)
                    {
                        // Define o volume inicial
                        SetVideoVolume(initialVolume);
                        
                        // Adiciona evento para esconder o splash quando o vídeo terminar
                        videoPlayer.loopPointReached += OnVideoFinished;
                    }
                }
                
                // Show the splash screen at the start
                ShowSplashScreen();
        #else
                // No editor, só executa se disableInEditor estiver false
                if (!disableInEditor)
                {
                    // Encontra o VideoPlayer no objeto filho "Eptinho Video"
                    if (splashScreenObject != null)
                    {
                        videoPlayer = splashScreenObject.GetComponentInChildren<VideoPlayer>();
                        if (videoPlayer != null)
                        {
                            // Define o volume inicial
                            SetVideoVolume(initialVolume);
                            
                            // Adiciona evento para esconder o splash quando o vídeo terminar
                            videoPlayer.loopPointReached += OnVideoFinished;
                        }
                    }
                    
                    // Show the splash screen at the start
                    ShowSplashScreen();
                }
        #endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        // Remove o evento quando o objeto for destruído para evitar vazamentos de memória
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
        
        // Garante que o jogo seja despausado se o objeto for destruído durante o splash
        if (pauseGameDuringSplash && splashScreenObject != null && splashScreenObject.activeInHierarchy)
        {
            Time.timeScale = originalTimeScale;
            ResumeAllAudioSources();
            Debug.Log("Jogo e áudios despausados no OnDestroy do SplashManager");
        }
    }


    private void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Vídeo do splash screen terminou, escondendo splash...");
        HideSplashScreen();
        
        // Remove o evento para evitar vazamentos de memória
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void ShowSplashScreen()
    {
        if (splashScreenObject != null)
        {
            splashScreenObject.SetActive(true);
            
            // Pausa o jogo se a opção estiver habilitada
            if (pauseGameDuringSplash)
            {
                Time.timeScale = 0f;
                PauseAllAudioSources();
                Debug.Log("Jogo pausado durante o splash screen (incluindo áudios)");
            }
            
            // Marca que o splash foi exibido
            hasBeenShown = true;
        }
    }
    public void HideSplashScreen()
    {
        if (splashScreenObject != null)
        {
            splashScreenObject.SetActive(false);
            
            // Restaura o TimeScale original se estava pausado
            if (pauseGameDuringSplash)
            {
                Time.timeScale = originalTimeScale;
                ResumeAllAudioSources();
                Debug.Log("Jogo despausado após splash screen (incluindo áudios)");
            }
        }
    }

    /// <summary>
    /// Define o volume do vídeo
    /// </summary>
    /// <param name="volume">Volume entre 0 (mudo) e 1 (máximo)</param>
    public void SetVideoVolume(float volume)
    {
        if (videoPlayer != null)
        {
            videoPlayer.SetDirectAudioVolume(0, volume);
        }
    }

    /// <summary>
    /// Obtém o volume atual do vídeo
    /// </summary>
    /// <returns>Volume atual entre 0 e 1</returns>
    public float GetVideoVolume()
    {
        if (videoPlayer != null)
        {
            return videoPlayer.GetDirectAudioVolume(0);
        }
        return 0f;
    }

    /// <summary>
    /// Muta ou desmuta o vídeo
    /// </summary>
    /// <param name="mute">True para mutar, false para desmutar</param>
    public void MuteVideo(bool mute)
    {
        if (videoPlayer != null)
        {
            videoPlayer.SetDirectAudioMute(0, mute);
        }
    }

    /// <summary>
    /// Reseta o controle de exibição do splash (útil para testes)
    /// </summary>
    public static void ResetSplashControl()
    {
        hasBeenShown = false;
        Debug.Log("Controle de splash resetado - splash pode ser exibido novamente");
    }

    /// <summary>
    /// Verifica se o splash já foi exibido nesta sessão
    /// </summary>
    /// <returns>True se já foi exibido</returns>
    public static bool HasBeenShown()
    {
        return hasBeenShown;
    }

    /// <summary>
    /// Pausa todos os AudioSources da cena, exceto o do splash
    /// </summary>
    private void PauseAllAudioSources()
    {
        // Limpa a lista anterior
        pausedAudioSources.Clear();
        
        // Encontra todos os AudioSources ativos na cena
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        
        foreach (AudioSource audioSource in allAudioSources)
        {
            // Não pausa o áudio do próprio splash screen
            if (audioSource != null && audioSource.isPlaying && 
                !IsAudioFromSplash(audioSource))
            {
                audioSource.Pause();
                pausedAudioSources.Add(audioSource);
                Debug.Log($"AudioSource pausado: {audioSource.gameObject.name}");
            }
        }
    }

    /// <summary>
    /// Verifica se o AudioSource pertence ao splash screen
    /// </summary>
    private bool IsAudioFromSplash(AudioSource audioSource)
    {
        if (splashScreenObject == null) return false;
        
        // Verifica se o AudioSource está dentro da hierarquia do splash
        Transform current = audioSource.transform;
        while (current != null)
        {
            if (current.gameObject == splashScreenObject)
                return true;
            current = current.parent;
        }
        
        return false;
    }

    /// <summary>
    /// Despausa todos os AudioSources que foram pausados
    /// </summary>
    private void ResumeAllAudioSources()
    {
        foreach (AudioSource audioSource in pausedAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.UnPause();
                Debug.Log($"AudioSource despausado: {audioSource.gameObject.name}");
            }
        }
        
        // Limpa a lista após despausar
        pausedAudioSources.Clear();
    }


}

    
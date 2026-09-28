using UnityEngine;

public class PantallaTutorial : MonoBehaviour
{
    [Header("Referencias de UI")]
    [Tooltip("Arrastra aqui el Canvas o Panel 3D con el texto de la mision")]
    [SerializeField] private GameObject panelAviso;
    [SerializeField] private GameObject panelTutorial;

    [Header("Sonido (Opcional)")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sonidoAbrir;

    private bool estaAbierto = false;

    void Start()
    {
        // El panel debe iniciar oculto en la escena
        if (panelAviso != null && panelTutorial != null)
        {
            panelAviso.SetActive(false);
            panelTutorial.SetActive(false);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Metodo que invoca CameraPointerManager cuando el Gaze termina
    public void OnPointerClickXR()
    {
        if (panelAviso == null && panelTutorial == null) return;

        // Alternar visibilidad (si esta abierto lo cierra, si esta cerrado lo abre)
        estaAbierto = !estaAbierto;
        panelAviso.SetActive(estaAbierto);
        panelTutorial.SetActive(estaAbierto);

        // Reproducir sonido
        if (audioSource != null && sonidoAbrir != null)
        {
            audioSource.PlayOneShot(sonidoAbrir);
        }
    }

    
    public void OnPointerEnterXR()
    {
    
    }

    public void OnPointerExitXR()
    {
    }
}

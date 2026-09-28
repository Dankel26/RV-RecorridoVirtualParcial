using UnityEngine;

public class PantallaTutorial : MonoBehaviour
{
    [Header("Referencias de UI")]
    [Tooltip("Arrastra aquí el Canvas o Panel 3D con el texto de la misión")]
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

    // Método que invoca tu CameraPointerManager cuando el Gaze termina
    public void OnPointerClickXR()
    {
        if (panelAviso == null && panelTutorial == null) return;

        // Alternar visibilidad (si está abierto lo cierra, si está cerrado lo abre)
        estaAbierto = !estaAbierto;
        panelAviso.SetActive(estaAbierto);
        panelTutorial.SetActive(estaAbierto);

        // Reproducir sonido si está asignado
        if (audioSource != null && sonidoAbrir != null)
        {
            audioSource.PlayOneShot(sonidoAbrir);
        }
    }

    // Feedback visual opcional cuando el puntero pasa sobre la pantalla
    public void OnPointerEnterXR()
    {
        // Puedes agregar lógica para que la pantalla brille o resalte
    }

    public void OnPointerExitXR()
    {
    }
}

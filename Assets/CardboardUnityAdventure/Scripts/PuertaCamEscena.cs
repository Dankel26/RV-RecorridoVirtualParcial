using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaCamEscena : MonoBehaviour
{
    [Header("Configuración de Navegación")]
    [Tooltip("Número de índice de la escena a la que quieres ir (según el Build Profiles)")]
    public int numeroEscena = 2;

    // Tu CameraPointerManager llama este método automáticamente cuando el Gaze se llena
    public void OnPointerClickXR()
    {
        Debug.Log("Cargando escena: " + numeroEscena);
        SceneManager.LoadScene(numeroEscena);
    }

    // Efecto visual opcional al mirar la puerta
    public void OnPointerEnterXR()
    {
        // Puedes encender un indicador o hacer sonar un efecto ligero
    }

    public void OnPointerExitXR()
    {
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaCamEscena : MonoBehaviour
{
    [Header("Configuracion de Navegacion")]
    [Tooltip("Numero de indice de la escena a la que quieres ir (segun el Build Profiles)")]
    public int numeroEscena = 2;

    
    public void OnPointerClickXR()
    {
        Debug.Log("Cargando escena: " + numeroEscena);
        SceneManager.LoadScene(numeroEscena);
    }

    
    public void OnPointerEnterXR()
    {
     
    }

    public void OnPointerExitXR()
    {
    }
}

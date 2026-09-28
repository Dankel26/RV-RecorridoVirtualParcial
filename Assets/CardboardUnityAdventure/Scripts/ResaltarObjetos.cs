using UnityEngine;

public class ResaltarObjetos : MonoBehaviour
{
    [Header("Configuracion del Resalte")]
    [Tooltip("Color que tomara el objeto al mirarlo")]
    [SerializeField] private Color colorResaltado = Color.cyan;
    
    [Tooltip("Si se activa, usara brillo de emision (HDR). Si no, cambiara el color base del material.")]
    [SerializeField] private bool usarEmision = true;
    
    [Range(0.5f, 3f)]
    [SerializeField] private float intensidadBrillo = 1.5f;

    private Renderer renderizador;
    private Color colorOriginal;
    private Color colorEmisionOriginal;
    private Material materialInstancia;

    void Start()
    {
        // Buscar el Renderer en este objeto o en sus hijos inmediatos
        renderizador = GetComponent<Renderer>();
        if (renderizador == null)
        {
            renderizador = GetComponentInChildren<Renderer>();
        }

        if (renderizador != null)
        {
            materialInstancia = renderizador.material; // Crea una copia local para no afectar a otros objetos con el mismo material

            if (materialInstancia.HasProperty("_Color"))
            {
                colorOriginal = materialInstancia.color;
            }

            if (materialInstancia.HasProperty("_EmissionColor"))
            {
                colorEmisionOriginal = materialInstancia.GetColor("_EmissionColor");
            }
        }
    }

    // Este metodo llama automaticamente CameraPointerManager al mirar el objeto
    public void OnPointerEnterXR()
    {
        if (materialInstancia == null) return;

        if (usarEmision && materialInstancia.HasProperty("_EmissionColor"))
        {
            materialInstancia.EnableKeyword("_EMISSION");
            materialInstancia.SetColor("_EmissionColor", colorResaltado * intensidadBrillo);
        }
        else if (materialInstancia.HasProperty("_Color"))
        {
            materialInstancia.color = colorResaltado;
        }
    }

    // Este metodo llama automaticamente CameraPointerManager al quitar la vista
    public void OnPointerExitXR()
    {
        if (materialInstancia == null) return;

        if (usarEmision && materialInstancia.HasProperty("_EmissionColor"))
        {
            materialInstancia.SetColor("_EmissionColor", colorEmisionOriginal);
        }
        else if (materialInstancia.HasProperty("_Color"))
        {
            materialInstancia.color = colorOriginal;
        }
    }

    private void OnDestroy()
    {
        // Limpieza de memoria para no saturar dispositivos moviles
        if (materialInstancia != null)
        {
            Destroy(materialInstancia);
        }
    }
}

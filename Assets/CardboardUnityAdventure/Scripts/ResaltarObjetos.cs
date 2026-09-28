using UnityEngine;

public class ResaltarObjetos : MonoBehaviour
{
    [Header("Configuración del Resalte")]
    [Tooltip("Color que tomará el objeto al mirarlo (Cian o Amarillo recomendados)")]
    [SerializeField] private Color colorResaltado = Color.cyan;
    
    [Tooltip("Si se activa, usará brillo de emisión (HDR). Si no, cambiará el color base del material.")]
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

    // Este método lo llama automáticamente tu CameraPointerManager al mirar el objeto
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

    // Este método lo llama automáticamente tu CameraPointerManager al quitar la vista
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
        // Limpieza de memoria para no saturar dispositivos móviles
        if (materialInstancia != null)
        {
            Destroy(materialInstancia);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClimaxManager : MonoBehaviour
{
    public static ClimaxManager Instance;

    [Header("Objetivo de Mision")]
    public int totalBateriasRequeridas = 3;
    private int bateriasInstaladas = 0;

    [Header("Iluminacion y Climax")]
    public Light[] lucesPunto;
    public Color colorAmbienteOscuro = new Color(0.05f, 0.08f, 0.15f);
    public Color colorAmbienteRestablecido = new Color(0.8f, 0.85f, 0.9f);
    public Color colorLuzFinal = Color.white;
    
    public float intensidadLuzInicial = 0.1f;
    public float intensidadLuzFinal = 1.2f;
    public float tiempoTransicionLuz = 2.5f;

    [Header("UI de Victoria / Final")]
    public GameObject canvasVictoria;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoPowerUp;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    void Start()
    {
        // Estado inicial: Luz ambiental tenue
        RenderSettings.ambientLight = colorAmbienteOscuro;

        // Atenuar las 4 Point Lights al inicio
        foreach (Light luz in lucesPunto)
        {
            if (luz != null)
            {
                luz.intensity = intensidadLuzInicial;
            }
        }

        if (canvasVictoria != null) canvasVictoria.SetActive(false);
    }

    public void RegistrarBateriaInstalada()
    {
        bateriasInstaladas++;
        Debug.Log("Bateria colocada. Total: " + bateriasInstaladas + "/" + totalBateriasRequeridas);

        if (bateriasInstaladas >= totalBateriasRequeridas)
        {
            ActivarClimaxGenerador();
        }
    }

    private void ActivarClimaxGenerador()
    {
        if (audioSource != null && sonidoPowerUp != null)
        {
            audioSource.PlayOneShot(sonidoPowerUp);
        }

        StartCoroutine(TransicionRestablecerLuz());
    }

    private IEnumerator TransicionRestablecerLuz()
    {
        float tiempo = 0f;

        while (tiempo < tiempoTransicionLuz)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / tiempoTransicionLuz;

            // Interpolacion de la luz ambiental
            RenderSettings.ambientLight = Color.Lerp(colorAmbienteOscuro, colorAmbienteRestablecido, t);

            // Animar las 4 Point Lights en paralelo
            foreach (Light luz in lucesPunto)
            {
                if (luz != null)
                {
                    luz.intensity = Mathf.Lerp(intensidadLuzInicial, intensidadLuzFinal, t);
                    luz.color = Color.Lerp(luz.color, colorLuzFinal, t);
                }
            }

            yield return null;
        }

        // Mostrar UI de finalizacion
        if (canvasVictoria != null)
        {
            canvasVictoria.SetActive(true);
        }
    }
}

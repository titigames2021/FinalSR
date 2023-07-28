using UnityEngine;
using System.IO;

public class ScreenshotCapture : MonoBehaviour
{
    // Directorio de destino para guardar la captura de pantalla
    public string screenshotDirectory = "Screenshots";

    // Tecla para activar la captura de pantalla
    public KeyCode captureKey = KeyCode.F12;

    private void Update()
    {
        // Verificar si se presionó la tecla para capturar la pantalla
        if (Input.GetKeyDown(captureKey))
        {
            // Crear el directorio si no existe
            if (!Directory.Exists(screenshotDirectory))
                Directory.CreateDirectory(screenshotDirectory);

            // Generar un nombre único para el archivo de la captura de pantalla
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            string filename = Path.Combine(screenshotDirectory, "Screenshot_" + timestamp + ".png");

            // Capturar la pantalla completa y guardarla como una imagen
            ScreenCapture.CaptureScreenshot(filename);

            Debug.Log("Captura de pantalla guardada en: " + filename);
        }
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo; // Arraste o Player aqui no Inspector

    public Vector3 offset = new Vector3(0, 0, -10); // Força o afastamento
                                                    // Mude o valor de suavidade para um número alto (ex: 1) para ELIMINAR A SUAVIZAÇÃO temporariamente.
    public float suavidade = 1f; // <-- Mude para 1.0f para um seguimento duro/imediato.

    void LateUpdate()
    {
        if (alvo != null)
        {
            Vector3 posicaoDesejada = alvo.position + offset;

            // Mude o 'suavidade' para 1.0f para testar se a câmera está seguindo o alvo IMEDIATAMENTE.
            Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, 1.0f);

            transform.position = new Vector3(posicaoSuavizada.x, posicaoSuavizada.y, -10f);
        }
    }
}
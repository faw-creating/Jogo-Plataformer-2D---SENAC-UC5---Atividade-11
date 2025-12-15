using UnityEngine; // Importa as funcionalidades básicas da Unity

public class CameraFollow : MonoBehaviour
{
    [Header("Configurações de Seguimento")]
    public Transform alvo; // O Player (arraste no Inspector)
    public Vector3 offset = new Vector3(0, 0, -10); // Afastamento da câmera

    [Range(0.01f, 1f)]
    public float suavidade = 0.125f; // Fator de suavização (0.5 é bom para começar)

    void LateUpdate() // LateUpdate é chamado após todos os Updates
    {
        if (alvo != null)
        {
            Vector3 posicaoDesejada = alvo.position + offset;

            // **CORREÇÃO:** Usamos Time.deltaTime para suavizar o movimento de forma gradual.
            Vector3 posicaoSuavizada = Vector3.Lerp(
                transform.position,
                posicaoDesejada,
                suavidade * Time.deltaTime * 5f // 5f é um multiplicador para tornar a câmera mais rápida
            );

            // Mantém o Z fixo em -10
            transform.position = new Vector3(posicaoSuavizada.x, posicaoSuavizada.y, -10f);
        }
    }
} // Fim da classe CameraFollow
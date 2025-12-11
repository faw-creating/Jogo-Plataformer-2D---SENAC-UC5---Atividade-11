using UnityEngine; // Importa as funcionalidades básicas

// Este script é anexado a qualquer objeto que o jogador deve coletar.
public class Collectible : MonoBehaviour // Início da classe Collectible
{
    [Header("Configurações do Coletável")]
    // Define quantos pontos este item adiciona ao ser coletado.
    // Você pode mudar este valor no Inspector para cada tipo de moeda (Prefab).
    public int pontosParaAdicionar = 1;

    // Define o nome da tag do objeto que pode coletar este item.
    private const string PlayerTag = "Player"; // Tag do jogador

    // Chamado quando algo entra no Trigger (Gatilho) deste objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player".
        if (collision.CompareTag(PlayerTag))
        {
            WhenCollect(); // Chama o método para lidar com a coleta
        }
    }

    // Método que lida com a lógica de coleta (o que acontece quando o item é pego).
    private void WhenCollect()
    {
        // 1. ADICIONAR PONTOS:
        // Verifica se o GameManager existe e o chama para adicionar os pontos.
        if (GameManager.Instance != null)
        {
            // O GameManager.Instance é estático, permitindo o acesso de qualquer lugar.
            GameManager.Instance.AdicionarPontos(pontosParaAdicionar);
        }

        // 2. DESTROI o objeto coletável (removendo-o da cena).
        Destroy(gameObject);
    }
} // Fim da classe Collectible
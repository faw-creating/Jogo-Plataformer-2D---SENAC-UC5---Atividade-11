using UnityEngine; // Importa as funcionalidades básicas da Unity
using TMPro; // Importa o TextMeshPro (ESSENCIAL para texto UI)

// O GameManager será responsável por guardar o estado do jogo (pontuação, vida, etc.)
public class GameManager : MonoBehaviour // Início da classe GameManager
{
    // O padrão Singleton: Garante que só exista uma instância e permite acesso fácil de outros scripts.
    public static GameManager Instance;

    [Header("Componentes UI")] // Cabeçalho para o Inspector
    // Variável para conectar o texto da pontuação (arraste o TextMeshProUGUI aqui no Inspector)
    public TextMeshProUGUI textoPontuacao;

    // A pontuação atual é privada, mas visível no Inspector para debug ([SerializeField])
    [SerializeField]
    private int pontuacaoAtual = 0;

    // --- Singleton Pattern (Garante que só exista um GameManager) ---
    void Awake() // Chamado antes de Start
    {
        if (Instance == null)
        {
            Instance = this; // Define esta instância como a única
            // IMPORTANTE: Mantém o GameManager vivo ao carregar novas cenas (Level1 -> GameOver)
            DontDestroyOnLoad(gameObject); // Evita que o GameManager seja destruído ao carregar uma nova cena
        }
        else
        {
            // Se já existir um GameManager, destrói este (a cópia)
            Destroy(gameObject); // Garante que só haja um GameManager
        }

        Debug.Log("GameManager Awake chamado e Singleton estabelecido.");
        // Atualiza o texto na tela ao iniciar o jogo
        AtualizarTextoPontuacao();
    }

    // Método que outros scripts (como Collectible.cs) vão chamar para adicionar pontos
    public void AdicionarPontos(int quantidade)
    {
        pontuacaoAtual += quantidade; // Aumenta a pontuação
        Debug.Log($"Pontuação atualizada: {pontuacaoAtual}");
        AtualizarTextoPontuacao(); // Atualiza o texto na tela imediatamente
    }

    // Atualiza o componente de texto na tela
    private void AtualizarTextoPontuacao() // Método privado
    {
        if (textoPontuacao != null)
        {
            // Formata o texto que será exibido
            textoPontuacao.text = $"Moedas: {pontuacaoAtual}";
        }
        else
        {
            // Aviso de debug caso o texto não tenha sido conectado no Inspector
            Debug.LogWarning("O textoPontuacao do GameManager está nulo! Conecte-o no Inspector.");
        }
    }

    // Novo: Salva o nome da cena atual como Checkpoint
    public void SalvarCheckpoint(string nomeDaCena)
    {
        PlayerPrefs.SetString("CheckpointScene", nomeDaCena);
        PlayerPrefs.Save(); // Garante que o disco salve imediatamente
        Debug.Log("Checkpoint salvo: " + nomeDaCena);
    }
}
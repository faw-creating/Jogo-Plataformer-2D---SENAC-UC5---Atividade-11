using System.Collections; // NECESSÁRIO para usar o "IEnumerator" (o delay)
using UnityEngine; // NECESSÁRIO para tudo do Unity
using UnityEngine.SceneManagement; // NECESSÁRIO para trocar de cena

public class LevelEnd : MonoBehaviour // Classe responsável por gerenciar o fim do nível
{
    private const string PlayerTag = "Player"; // Tag do jogador

    [Header("Configurações do Fim de Jogo")] // Cabeçalho no Inspetor
    public bool isVictoryTrigger = true; // Define se é vitória ou derrota
    public string nextSceneName = "GameOver"; // Nome da próxima cena (se vitória)
    public float tempoDeEspera = 2.0f; // Tempo de delay (em segundos)

    private bool levelEncerrado = false; // Trava para impedir repetições

    void Awake() // Chamado quando o objeto é inicializado
    { // Início do Awake
        Debug.Log("LevelEnd pronto."); // Mensagem de debug
    } // Fim do Awake

    private void OnTriggerEnter2D(Collider2D collision) // Detecta colisões 2D
    {
        // Só entra se for o Player E se o level ainda não tiver encerrado
        if (collision.CompareTag(PlayerTag) && !levelEncerrado) // Verifica a tag do jogador
        { // Início do IF
            StartCoroutine(ProcessarFimDeJogo()); // Inicia a rotina de fim de jogo com delay
        } // Fim do IF
    } // Fim do OnTriggerEnter2D

    // A MÁGICA DO DELAY ACONTECE AQUI
    IEnumerator ProcessarFimDeJogo() // Rotina para processar o fim do jogo com delay
    {
        levelEncerrado = true; // Trava imediata: Ninguém mais chama essa função

        if (isVictoryTrigger) // Se for vitória
        { // Início do IF
            Debug.Log("Vitória! Esperando " + tempoDeEspera + " segundos..."); // Mensagem de vitória

            // Pausa o código aqui por X segundos, mas o jogo continua rodando
            yield return new WaitForSeconds(tempoDeEspera); // Espera o tempo definido

            Debug.Log("Carregando próxima cena: " + nextSceneName); // Mensagem de carregamento
            SceneManager.LoadScene(nextSceneName); // Carrega a próxima cena
        } // Fim do IF
        else // Se for derrota
        { // Início do ELSE
            Debug.Log("Derrota! Reiniciando em instantes..."); // Mensagem de derrota

            // Delay para derrota também
            yield return new WaitForSeconds(tempoDeEspera / 2); // Espera metade do tempo na derrota (opcional)

            SceneManager.LoadScene("GameOver"); // Carrega a cena de Game Over
        } // Fim do ELSE
    } // Fim do IEnumerator
} // Fim da classe LevelEnd
using UnityEngine; // ESSENCIAL para scripts Unity
using UnityEngine.SceneManagement; // ESSENCIAL para carregar cenas e manipula-las

// MUDAMOS O NOME: A classe agora é 'MenuManager' para não conflitar com o 'SceneManager' da Unity
public class MenuManager : MonoBehaviour // MonoBehaviour conecta o script a um GameObject na cena
{ // Início da classe MenuManager
    
    // Esta função será chamada pelo botão "Iniciar"
    // O nome da cena (nomeDaCena) deve ser escrito EXATAMENTE
    // como está no seu arquivo de cena (ex: "Level1")

    void Awake()
    {
        Debug.Log("MenuManager Awake chamado, código iniciado"); // Log para depuração
    }

    public void CarregarCena(string nomeDaCena)
    { // Início da função CarregarCena
        // Agora o C# sabe que estamos usando o SceneManager DA UNITY
        SceneManager.LoadScene(nomeDaCena); // Carrega a cena com o nome fornecido
        Debug.Log("O botão PLAY foi clicado! Iniciando " + nomeDaCena); // Mensagem de debug 1
    } // Fim da função CarregarCena

    public void CarregarUltimoCheckpoint()
    {
        // Procura o nome da fase salva. Se não achar nada, volta pro Level1_Novo
        string faseSalva = PlayerPrefs.GetString("CheckpointScene", "Level1_Novo");
        SceneManager.LoadScene(faseSalva);
        Debug.Log("Carregando o progresso salvo: " + faseSalva);
    }

    // Esta função será chamada pelo botão "Reiniciar"
    public void ReiniciarCenaAtual()
    { // Início da função ReiniciarCenaAtual
        // Pega a cena que está ativa AGORA
        Scene cenaAtual = SceneManager.GetActiveScene(); // Pega a cena atual
        // Recarrega a cena usando o nome dela
        SceneManager.LoadScene(cenaAtual.name); // Recarrega a cena atual
        Debug.Log("O botão REINICIAR foi clicado! Reiniciando-se jogo..."); // Mensagem de debug 2
    } // Fim da função ReiniciarCenaAtual

    // Esta função será chamada pelo botão "GameOver"
    public void GameOver()
    { // Início da função GameOver
        // Carrega a cena "GameOver"
        SceneManager.LoadScene("GameOver"); // Carrega a cena de Game Over
        Debug.Log("Game Over! Carregando a cena de Game Over..."); // Mensagem de debug 3
    } // Fim da função GameOver

    // Esta função será chamada pelo botão "Sair"
    public void SairDoJogo()
    { // Início da função SairDoJogo
        // Log para sabermos que funcionou no editor
        Debug.Log("O botão SAIR foi clicado! Fechando-se jogo..."); // Mensagem de debug 4
        // Application.Quit() só funciona no jogo "buildado" (o .exe), não funciona no Editor Unity.
        Application.Quit(); // Fecha o jogo
    } // Fim da função SairDoJogo

} // Fim da classe MenuManager
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para gerenciar cenas

public class ReturnButton : MonoBehaviour
{
    // Define se este é um botão de "Voltar ao Início" ou "Último Checkpoint"
    public bool voltarAoNivel1 = false;

    // Você deve chamar esta função no clique do botão na tela de Game Over/Menu
    public void Retornar()
    {
        if (voltarAoNivel1)
        {
            // Volta sempre para a primeira cena (índice 1, assumindo Menu é 0)
            SceneManager.LoadScene(1);
            Debug.Log("Voltando ao Nível 1.");
        }
        else
        {
            // O ideal para Checkpoint: Pega o índice salvo do último nível jogado.
            // Aqui, usamos o GameManager para carregar o nome da última cena ou a cena atual.
            // Por enquanto, voltamos à cena atual (reiniciar o nível):
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Voltando ao último nível/Reiniciando.");
        }
    }
}
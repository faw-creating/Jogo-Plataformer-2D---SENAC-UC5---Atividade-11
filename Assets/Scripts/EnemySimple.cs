using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [Header("--- Configuração de Movimento ---")]
    public float velocidade = 2.0f;
    public float distanciaPatrulha = 3.0f;
    public float distanciaAtaque = 6.0f; // Distância para começar a perseguir

    [Header("--- Referências ---")]
    public Transform player; // O alvo (Capsy)
    private float posicaoInicialX;
    private bool indoParaDireita = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        posicaoInicialX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Tenta achar o Player sozinho se você esqueceu de arrastar
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        // 1. Calcula a distância até o jogador
        float distanciaDoPlayer = 100f; // Valor alto padrão
        if (player != null)
        {
            distanciaDoPlayer = Vector2.Distance(transform.position, player.position);
        }

        // 2. DECISÃO: Perseguir ou Patrulhar?
        if (distanciaDoPlayer < distanciaAtaque)
        {
            PerseguirPlayer();
        }
        else
        {
            MoverPatrulha();
        }
    }

    void PerseguirPlayer()
    {
        // Move em direção ao Player
        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);

        // Vira o sprite para olhar para o player
        if (player.position.x > transform.position.x)
        {
            VirarSprite(false); // Olha para a direita
        }
        else
        {
            VirarSprite(true); // Olha para a esquerda
        }
    }

    void MoverPatrulha()
    {
        float limiteDireita = posicaoInicialX + distanciaPatrulha;
        float limiteEsquerda = posicaoInicialX - distanciaPatrulha;

        if (indoParaDireita)
        {
            transform.Translate(Vector2.right * velocidade * Time.deltaTime);
            if (transform.position.x >= limiteDireita)
            {
                indoParaDireita = false;
                VirarSprite(true);
            }
        }
        else
        {
            transform.Translate(Vector2.left * velocidade * Time.deltaTime);
            if (transform.position.x <= limiteEsquerda)
            {
                indoParaDireita = true;
                VirarSprite(false);
            }
        }
    }

    void VirarSprite(bool olharEsquerda)
    {
        if (spriteRenderer != null) spriteRenderer.flipX = olharEsquerda;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Player"))
        {
            Debug.Log("Derrota por Robotino! Chamando Game Over...");

            // Procura o objeto com o MenuManager (geralmente o GameManager)
            MenuManager menu = Object.FindFirstObjectByType<MenuManager>();

            if (menu != null)
            {
                // Chama a função GameOver que carrega a cena "GameOver"
                menu.GameOver();
            }
            else
            {
                // Se o MenuManager não foi encontrado, reinicia como fallback
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
        }
    }
}
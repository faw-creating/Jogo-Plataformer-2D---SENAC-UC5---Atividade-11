using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [Header("Configuração de Movimento")]
    public float velocidade = 2.0f;
    public float distanciaPatrulha = 3.0f;
    public float distanciaAtaque = 6.0f;

    [Header("Referências")]
    public Transform player;
    private float posicaoInicialX;
    private bool indoParaDireita = true;
    private SpriteRenderer spriteRenderer;

    // Variável para evitar spam no console (logar apenas a cada 1 segundo)
    private float tempoUltimoLog = 0f;

    void Start()
    {
        posicaoInicialX = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        float distanciaDoPlayer = 100f;
        if (player != null)
        {
            distanciaDoPlayer = Vector2.Distance(transform.position, player.position);

            // --- DEBUG LOGS (Estético) ---
            // Se estiver perto (distancia < 3) e já passou 1 segundo desde o último aviso
            if (distanciaDoPlayer < 3.0f && Time.time > tempoUltimoLog + 1.0f)
            {
                Debug.Log($"⚠️ Robotino detectou intruso! Distância: {distanciaDoPlayer:F1}m");
                tempoUltimoLog = Time.time;
            }
        }

        // DECISÃO
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
        transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);

        // Vira o sprite
        if (player.position.x > transform.position.x) VirarSprite(false);
        else VirarSprite(true);
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
            Debug.Log("❌ CAPSY FOI PEGO! Game Over."); // Log estético de derrota

            MenuManager menu = Object.FindFirstObjectByType<MenuManager>();
            if (menu != null) menu.GameOver();
            else UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    // --- PARTE VISUAL (GIZMOS) ---
    // Isso desenha a linha vermelha na cena quando você clica no inimigo
    void OnDrawGizmosSelected()
    {
        // Desenha uma linha representando a área de patrulha
        Gizmos.color = Color.yellow;
        // Onde ele nasceu (aproximado se o jogo não começou) ou posição inicial salva
        float centro = Application.isPlaying ? posicaoInicialX : transform.position.x;

        Vector3 pontoEsq = new Vector3(centro - distanciaPatrulha, transform.position.y, 0);
        Vector3 pontoDir = new Vector3(centro + distanciaPatrulha, transform.position.y, 0);

        Gizmos.DrawLine(pontoEsq, pontoDir);

        // Desenha esferas nas pontas para ficar bonitinho
        Gizmos.DrawSphere(pontoEsq, 0.2f);
        Gizmos.DrawSphere(pontoDir, 0.2f);

        // Desenha o raio de ataque (Círculo Vermelho)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaAtaque);
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações")]
    public float velocidadeMovimento = 7.0f;
    public float forcaPulo = 12.0f;

    [Header("Detecção de Chão")]
    public Transform verificadorChao;
    public LayerMask layerChao;
    public float raioVerificador = 0.2f;

    private Rigidbody2D rb;
    private float inputHorizontal;
    private bool estaNoChao;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. INPUT (A e D ou Setas)
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        // 2. PULO
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            // Zera a velocidade Y antes de pular para garantir altura consistente
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // 3. CHECAR CHÃO
        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificador, layerChao);

        // 4. MOVER (Correção: Atribuição direta para teste)
        // Removemos o Lerp para garantir que ele ande. Se andar, depois suavizamos.
        rb.linearVelocity = new Vector2(inputHorizontal * velocidadeMovimento, rb.linearVelocity.y);
    }
}
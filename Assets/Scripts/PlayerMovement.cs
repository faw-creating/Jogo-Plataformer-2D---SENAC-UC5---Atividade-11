// Inicio do c�digo PlayerMovement.cs
// importante: Este script deve ser anexado a um GameObject que possua um componente Rigidbody2D para funcionar corretamente.
using UnityEngine; // Importa namespace "UnityEngine" (principal) para acesso �s funcionalidades da Unity

// Este script controla o movimento horizontal e o salto do jogador.
public class PlayerMovement : MonoBehaviour // In�cio da classe PlayerMovement
{
    // Campos p�blicos que podem ser ajustados no Inspector da Unity
    [Header("Configura��es de Movimento")] // Cabe�alho para organiza��o no Inspector
    public float velocidadeMovimento = 7.0f; // Velocidade m�xima horizontal
    public float forcaPulo = 12.0f;          // For�a aplicada ao pular
    public float suavizacaoMovimento = 0.5f; // Fator para tornar a parada/in�cio suave (entre 0 e 1)

    // Componentes e Vari�veis Privadas
    private Rigidbody2D rb;             // Refer�ncia ao componente Rigidbody2D
    private float inputHorizontal;      // Valor de entrada do teclado (-1 para esquerda, 1 para direita)
    private bool estaNoChao = false;    // Verifica se o jogador est� tocando o ch�o

    // Refer�ncia para o transform que detecta o ch�o (opcional, mas recomendado para 2D)
    [Header("Detec��o de Ch�o")] // Cabe�alho para organiza��o no Inspector
    public Transform verificadorChao; // Ponto de verifica��o do ch�o
    public LayerMask layerChao; // Qual Layer define o "ch�o"
    public float raioVerificador = 0.2f; // Raio do c�rculo para verifica��o do ch�o

    void Awake() // Chamado quando o script � carregado
    {
        Debug.Log("PlayerMovement Awake chamado, c�digo iniciado"); // Log para depura��o
        // Garante que a refer�ncia ao Rigidbody2D seja obtida
        rb = GetComponent<Rigidbody2D>(); // Tenta obter o componente Rigidbody2D anexado ao mesmo GameObject
        if (rb == null) // Verifica se o Rigidbody2D foi encontrado
        { // In�cio do if
            Debug.LogError("O Rigidbody2D est� faltando no Player! Corrija!"); // Log de erro se n�o encontrado
        } // Fim do if
    } // Fim do Awake

    // Update � chamado a cada frame e � usado para ler a entrada do usu�rio
    void Update() // In�cio do Update
    { // 1. MOVIMENTO HORIZONTAL E PULO
        // 1. LER ENTRADA HORIZONTAL
        inputHorizontal = Input.GetAxisRaw("Horizontal"); // Obt�m entrada do teclado (A/D ou Setas Esquerda/Direita)

        // 2. DETEC��O DE PULO (tecla Espa�o)
        // O jogador s� pode pular se estiver no ch�o
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) // Verifica se a tecla Espa�o foi pressionada e se est� no ch�o
        { // In�cio do if
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Zera a velocidade vertical para garantir a for�a total
            rb.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse); // Aplica a for�a de pulo instantaneamente
        } // Fim do if
    } // Fim do Update

    // FixedUpdate � chamado em intervalos fixos (melhor para f�sica e Rigidbody)
    void FixedUpdate() // In�cio do FixedUpdate
    {
        // 1. VERIFICAR SE EST� NO CH�O
        // Usa um c�rculo invis�vel no p� do personagem para checar a colis�o com o ch�o
        estaNoChao = Physics2D.OverlapCircle(verificadorChao.position, raioVerificador, layerChao); // Verifica se o c�rculo est� colidindo com o ch�o

        // 2. CALCULAR A VELOCIDADE ALVO (sem a suaviza��o)
        Vector2 velocidadeAlvo = new Vector2(inputHorizontal * velocidadeMovimento, rb.linearVelocity.y); // Calcula a velocidade desejada com base na entrada do jogador

        // 3. APLICAR SUAVIZA��O E MOVIMENTO
        // Usamos Lerp (Interpola��o Linear) para mover a velocidade atual para a velocidade alvo de forma suave.
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, velocidadeAlvo, suavizacaoMovimento * Time.fixedDeltaTime); // Aplica a suaviza��o ao movimento
    } // Fim do FixedUpdate
} // Fim da classe PlayerMovement

// Fim do c�digo PlayerMovement.cs
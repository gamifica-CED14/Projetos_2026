using UnityEngine;

public class InimigoIA : MonoBehaviour
{
     // --- Configurações de patrulha --- 
    [Header("Patrulha")] 
    [SerializeField] private float velocidadePatrulha = 2f; 
    [SerializeField] private float distanciaPatrulha = 3f; 
 
    // --- Configurações de detecção --- 
    [Header("Detecção")] 
    [SerializeField] private float raioDeteccao = 4f; 
    [SerializeField] private float velocidadePerseguicao = 4f; 
 
    // --- Configurações de dano --- 
    [Header("Dano")] 
    [SerializeField] private int dano = 1; 
 
    // --- Variáveis internas --- 
    private enum Estado { Patrulhando, Perseguindo, Voltando } 
    private Estado estadoAtual = Estado.Patrulhando; 
 
    private Vector2 pontoInicial; 
    private bool movendoDireita = true; 
    private Transform personagem; 
 
    void Start() 
    { 
        // Guarda a posição inicial do inimigo 
        pontoInicial = transform.position; 
 
        // Busca o personagem pela tag 
        GameObject obj = GameObject.FindWithTag("Player"); 
        if (obj != null) 
            personagem = obj.transform; 
    } 
 
    void Update() 
    { 
        if (personagem == null) return; 
 
        float distanciaAoPersonagem = Vector2.Distance(transform.position, 
personagem.position); 
 
        // --- Máquina de estados da IA --- 
        switch (estadoAtual) 
        { 
            case Estado.Patrulhando: 
                Patrulhar(); 
 
                // Se o personagem entrou no raio de detecção, começa a perseguir 
                if (distanciaAoPersonagem <= raioDeteccao) 
                    estadoAtual = Estado.Perseguindo; 
                break; 
 
            case Estado.Perseguindo: 
                Perseguir(); 
 
                // Se o personagem saiu do raio, volta para o ponto inicial 
                if (distanciaAoPersonagem > raioDeteccao) 
                    estadoAtual = Estado.Voltando; 
                break; 
 
            case Estado.Voltando: 
                Voltar(); 
 
                // Se o personagem foi detectado de novo durante o retorno, persegue 
                if (distanciaAoPersonagem <= raioDeteccao) 
                    estadoAtual = Estado.Perseguindo; 
                break; 
        } 
    } 
 
    // --- Comportamento: Patrulha --- 
    void Patrulhar() 
    { 
        if (movendoDireita) 
        { 
            transform.Translate(Vector2.right * velocidadePatrulha * Time.deltaTime); 
 
            if (transform.position.x >= pontoInicial.x + distanciaPatrulha) 
                movendoDireita = false; 
        } 
        else 
        { 
            transform.Translate(Vector2.left * velocidadePatrulha * Time.deltaTime); 
 
            if (transform.position.x <= pontoInicial.x - distanciaPatrulha) 
                movendoDireita = true; 
        } 
    } 
 
    // --- Comportamento: Perseguição --- 
    void Perseguir() 
    { 
        // Move o inimigo em direção ao personagem 
        Vector2 direcao = (personagem.position - transform.position).normalized; 
        transform.Translate(direcao * velocidadePerseguicao * Time.deltaTime); 
    } 
 
    // --- Comportamento: Retorno ao ponto inicial --- 
    void Voltar() 
    { 
        Vector2 direcao = (pontoInicial - (Vector2)transform.position).normalized; 
        transform.Translate(direcao * velocidadePatrulha * Time.deltaTime); 
 
        // Quando chega perto o suficiente do ponto inicial, volta a patrulhar 
        float distanciaAoPontoInicial = Vector2.Distance(transform.position, pontoInicial); 
        if (distanciaAoPontoInicial < 0.2f) 
        { 
            transform.position = pontoInicial; 
            estadoAtual = Estado.Patrulhando; 
        } 
    } 
 
    // --- Dano ao colidir com o personagem --- 
    void OnCollisionEnter2D(Collision2D colisao) 
    { 
        if (colisao.gameObject.CompareTag("Player")) 
        { 
            HeartSystem vida = colisao.gameObject.GetComponent<HeartSystem>(); 
 
            if (vida != null) 
                vida.ReceberDano(dano); 
        } 
    } 
 
    // --- Desenha o raio de detecção na cena (só visível no Editor) --- 
    void OnDrawGizmosSelected() 
    { 
        Gizmos.color = Color.red; 
        Gizmos.DrawWireSphere(transform.position, raioDeteccao); 
 
        Gizmos.color = Color.yellow; 
        Vector3 inicio = Application.isPlaying ? (Vector3)pontoInicial : transform.position; 
        Gizmos.DrawLine(inicio + Vector3.left * distanciaPatrulha, 
                        inicio + Vector3.right * distanciaPatrulha); 
    } 
}
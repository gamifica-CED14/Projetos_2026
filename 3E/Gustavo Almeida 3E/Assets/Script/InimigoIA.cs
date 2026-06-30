using UnityEngine;

public class InimigoIA : MonoBehaviour
{
    [Header("Patrulha")] 
    [SerializeField] private float velocidadePatrulha = 2f; 
    [SerializeField] private float distanciaPatrulha = 3f; 
 
    [Header("Detecção")] 
    [SerializeField] private float raioDeteccao = 4f; 
    [SerializeField] private float velocidadePerseguicao = 4f; 
 
    [Header("Dano")] 
    [SerializeField] private int dano = 1; 
 
    private enum Estado { Patrulhando, Perseguindo, Voltando } 
    private Estado estadoAtual = Estado.Patrulhando; 
 
    private Vector2 pontoInicial; 
    private bool movendoDireita = true; 
    private Transform personagem; 
 
    void Start() 
    { 
        pontoInicial = transform.position; 
 
        GameObject obj = GameObject.FindWithTag("Player"); 
        if (obj != null) 
            personagem = obj.transform; 
    } 
 
    void Update() 
    { 
        if (personagem == null) return; 
 
        float distanciaAoPersonagem = Vector2.Distance(transform.position, 
personagem.position); 
 
        switch (estadoAtual) 
        { 
            case Estado.Patrulhando: 
                Patrulhar(); 
                if (distanciaAoPersonagem <= raioDeteccao) 
                    estadoAtual = Estado.Perseguindo; 
                break; 
 
            case Estado.Perseguindo: 
                Perseguir(); 
                if (distanciaAoPersonagem > raioDeteccao) 
                    estadoAtual = Estado.Voltando; 
                break; 
 
            case Estado.Voltando: 
                Voltar(); 
                if (distanciaAoPersonagem <= raioDeteccao) 
                    estadoAtual = Estado.Perseguindo; 
                break; 
        } 
    } 
 
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
 
    void Perseguir() 
    { 
        Vector2 direcao = (personagem.position - transform.position).normalized; 
        transform.Translate(direcao * velocidadePerseguicao * Time.deltaTime); 
    } 
 
    void Voltar() 
    { 
        Vector2 direcao = (pontoInicial - (Vector2)transform.position).normalized; 
        transform.Translate(direcao * velocidadePatrulha * Time.deltaTime); 
 
        float distanciaAoPontoInicial = Vector2.Distance(transform.position, pontoInicial); 
        if (distanciaAoPontoInicial < 0.2f) 
        { 
            transform.position = pontoInicial; 
            estadoAtual = Estado.Patrulhando; 
        } 
    } 
 
    void OnCollisionEnter2D(Collision2D colisao) 
    { 
        if (colisao.gameObject.CompareTag("Player")) 
        { 
            HeartSystem vida = colisao.gameObject.GetComponent<HeartSystem>(); 
            if (vida != null) 
                vida.ReceberDano(dano); 
        } 
    } 
 
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
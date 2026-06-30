using UnityEngine; 
 
public class Inimigo : MonoBehaviour 
{ 
    [SerializeField] private float velocidade = 2f; 
    [SerializeField] private float distanciaPatrulha = 3f; 
    [SerializeField] private int dano = 1; 
 
    private Vector2 pontoInicial; 
    private bool movendoDireita = true; 
 
    void Start() 
    { 
        pontoInicial = transform.position; 
    } 
 
    void Update() 
    { 
        Patrulhar(); 
    } 
 
    void Patrulhar() 
    { 
        if (movendoDireita) 
        { 
            transform.Translate(Vector2.right * velocidade * Time.deltaTime); 
 
            if (transform.position.x >= pontoInicial.x + distanciaPatrulha) 
                movendoDireita = false; 
        } 
        else 
        { 
            transform.Translate(Vector2.left * velocidade * Time.deltaTime); 
 
            if (transform.position.x <= pontoInicial.x - distanciaPatrulha) 
                movendoDireita = true; 
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
} 

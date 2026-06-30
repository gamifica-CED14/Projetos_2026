using UnityEngine;

public class ControlePlay : MonoBehaviour
{

    public float velocidade = 5f;
    public float forcaPulo = 10f;


    // Detecção de chão
    public Transform checadorChao;
    public float raioChao = 0.2f;
    public LayerMask layerChao;


    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float movimentoX;
    private bool noChao;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
void Update()
    {
        // Movimento horizontal
        movimentoX = Input.GetAxisRaw("Horizontal");


        // Detecta chão
        noChao = Physics2D.OverlapCircle(
            checadorChao.position,
            raioChao,
            layerChao
        );


        // Flip do personagem
        if (movimentoX > 0)
            sprite.flipX = false;
        else if (movimentoX < 0)
            sprite.flipX = true;


        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                forcaPulo
            );
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimentoX * velocidade,
            rb.linearVelocity.y
        );
    }
}

using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
     [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 10f;
    [Header("Detecção de Chão")]
    public Transform checadorChao;
    public float raioChao = 0.2f;
    public LayerMask layerChao;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private float movimentoX;
    private bool noChao;
    private int quantidadePulos = 0;
    public int maxPulos = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        movimentoX = Input.GetAxisRaw("Horizontal");
        // Detecta chão
        noChao = Physics2D.OverlapCircle(
            checadorChao.position,
            raioChao,
            layerChao
        );
        if (noChao)
        {
            quantidadePulos = 0;
        }
        if (movimentoX > 0)
            sprite.flipX = false;
        else if (movimentoX < 0)
            sprite.flipX = true;
        if (movimentoX != 0)
            anim.SetBool("walk", true);
        else
            anim.SetBool("walk", false);
        if (!noChao)
            anim.SetBool("jump", true);
        else
            anim.SetBool("jump", false);
        if (Input.GetKeyDown(KeyCode.Space)
            && quantidadePulos < maxPulos)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                forcaPulo
            );
            quantidadePulos++;
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimentoX * velocidade,
            rb.linearVelocity.y
        );
    }
    void OnDrawGizmosSelected()
    {
        if (checadorChao != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                checadorChao.position,
                raioChao
            );
        }
    }
}

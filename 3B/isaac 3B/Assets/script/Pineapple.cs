using UnityEngine;

public class Pineapple : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    public GameObject collected;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }
    // Quando algo encostar na maçã
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Verifica se foi o Player
        if (collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);
            // Coleta a maçã (destrói após 1 segundo)
            Destroy(gameObject, 0.3f);
        }
    }
}

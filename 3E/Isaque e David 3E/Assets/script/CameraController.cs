using UnityEngine; 
public class CameraController : MonoBehaviour 
{ 
// Referência ao Transform do personagem 
private Transform target; 
 
    // Variável interna usada pelo SmoothDamp 
    private Vector3 velocity = Vector3.zero; 
 
    // Velocidade de suavização (ajustável no Inspector) 
    [SerializeField] private float smoothTime = 0.1f; 
 
    void Start() 
    { 
        // Busca o objeto com a tag "Player" e pega seu Transform 
        target = GameObject.FindWithTag("Player").transform; 
    } 
 
    void Update() 
    { 
        // Calcula a posição desejada da câmera (mesma do personagem, mas recuada no eixo Z)
        Vector3 cameraPosition = target.position + new Vector3(0, 0, -10f); 
 
        // Move a câmera suavemente até a posição desejada 
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref 
velocity, smoothTime); 
    } 
}
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
 
 
public class CameraController : MonoBehaviour 
{ 
    private Transform target; 
    private Vector3 velocity = Vector3.zero; 
 
    [SerializeField] private float smoothTime = 0.1f; 
 
    void Start() 
    { 
        target = GameObject.FindWithTag("Player").transform; 
    } 
 
    void Update() 
    { 
        Vector3 cameraPosition = target.position + new Vector3(0, 0, -10f); 
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref 
velocity, smoothTime); 
    } 
} 

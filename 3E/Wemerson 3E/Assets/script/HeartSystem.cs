using UnityEngine;
using UnityEngine.UI; 
public class HeartSystem : MonoBehaviour 
{ 
    public int vida = 5; 
    public int vidaMaxima = 5; 
 
    public Image[] coracoes; 
    public Sprite cheio; 
    public Sprite vazio; 
 
    void Update() 
    { 
        if (vida > vidaMaxima) 
            vida = vidaMaxima; 
 
        AtualizarCoracoes(); 
    } 
 
    void AtualizarCoracoes() 
    { 
        for (int i = 0; i < coracoes.Length; i++) 
        { 
            coracoes[i].enabled = (i < vidaMaxima); 
 
            if (i < vida) 
                coracoes[i].sprite = cheio; 
            else 
                coracoes[i].sprite = vazio; 
        } 
    } 
 
    public void ReceberDano(int dano) 
    { 
        vida -= dano; 
 
        if (vida <= 0) 
        { 
            vida = 0; 
            Morrer(); 
        } 
    } 
 
    void Morrer() 
    { 
        Debug.Log("Personagem morreu!"); 
        gameObject.SetActive(false); 
    } 
} 
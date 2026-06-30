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
        // Garante que a vida nunca ultrapasse o máximo 
        if (vida > vidaMaxima)
            vida = vidaMaxima;

        AtualizarCoracoes();
    }

    void AtualizarCoracoes()
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            // Mostra ou esconde o coração dependendo da vida máxima atual 
            coracoes[i].enabled = (i < vidaMaxima);

            // Define se o coração aparece cheio ou vazio 
            if (i < vida)
                coracoes[i].sprite = cheio;
            else
                coracoes[i].sprite = vazio;
        }
    }

    // Método público para receber dano (chamado pelo inimigo) 
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
        // Aqui você pode adicionar: animação de morte, reiniciar a fase, etc. 
        gameObject.SetActive(false);
    }
}
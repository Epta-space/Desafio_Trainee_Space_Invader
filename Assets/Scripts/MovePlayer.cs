using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float velocidade;
    public GameObject projetilPrefab;
    public GameObject player;
    int[] move = new int[2];
    

    void Update()
    {
        if(move[0]==1)
        {
            player.transform.Translate(Vector2.right *-1* velocidade * Time.deltaTime);
        }
        if(move[1]==1)
        {
            player.transform.Translate(Vector2.right *1* velocidade * Time.deltaTime);
        }
    }
    public void fire()
    {
        Instantiate(projetilPrefab, player.transform.position, Quaternion.identity);
    }

    public void move_Left()
    {
        move[0] = 1;
    }

    public void move_Right()
    {
        move[1] = 1;
    }
    public void stop_move_Left()
    {
        move[0] = 0;
    }

    public void stop_move_Right()
    {
        move[1] = 0;
    }
    //Quando move[0] = 1 player move para a esquerda no void update
    //Quando move[1] = 1 player move para a direita no void update
    //facilita o controle da movimentação de forma contínua
}


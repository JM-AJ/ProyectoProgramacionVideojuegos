using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public int velocidad = 10;

    Rigidbody2D jugador; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jugador = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jugador.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * velocidad, jugador.linearVelocity.y*Time.deltaTime);
    }
}

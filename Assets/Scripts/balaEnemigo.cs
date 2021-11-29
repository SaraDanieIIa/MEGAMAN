using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaEnemigo : MonoBehaviour
{
    public float Speed = 20f;
    public int direccion = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= direccion*transform.right * Time.deltaTime * Speed * -1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

       // Destroy(this.gameObject);
    }
}

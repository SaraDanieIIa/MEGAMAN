using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaEnemigo2 : MonoBehaviour
{
    public float Speed = 20f;
    Rigidbody2D mybody;
    public int direccion = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= direccion * transform.right * Time.deltaTime * Speed ;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

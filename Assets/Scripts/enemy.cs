using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] CircleCollider2D detector;
    [SerializeField] GameObject player;
    [SerializeField] int vida;
    [SerializeField] GameManager Gm;

    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D chocando = Physics2D.OverlapCircle(transform.position, 6, LayerMask.GetMask("Player"));

        if(chocando != null)
        {
            myAnimator.SetBool("Volando", true);
        }
        else
        {
            myAnimator.SetBool("Volando", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 6);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            vida--;
            if (vida <= 0)
            {
                GameObject gm = GameObject.Find("GameManager");
                GameManager script = gm.GetComponent<GameManager>();
                script.Kill();
                myAnimator.SetTrigger("muerte");
                
            }
        }
    }
    void DieE()
    {
        Destroy(gameObject);
    }
}

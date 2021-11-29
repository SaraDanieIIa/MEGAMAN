using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    [SerializeField] GameObject balaP;
    [SerializeField] Sprite sprite;
    [SerializeField] Transform d1;
    [SerializeField] Transform playerTransform;
    Animator myanimator;
    bool isActive = true;
    bool keepShooting = false;
    private float timer = 0f;
    private float delay = 1f;
    [SerializeField] int vidas;
    private SpriteRenderer spriteRenderer;

    float eulerAngY;
    float eulerAngX;
    float eulerAngZ;
    void Start()
    {
        myanimator = GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = gameObject.transform.position.x - playerTransform.position.x;
        // Debug.Log(dist);
        eulerAngY = transform.localEulerAngles.y;
        eulerAngX = transform.localEulerAngles.x;
        eulerAngZ = transform.localEulerAngles.z;
        Debug.Log(eulerAngY+" "+eulerAngX+" "+eulerAngZ);
        if (keepShooting)
        {
            if (timer >= delay || timer == 0)
            {
                disparar();
                timer = 0;
            }
            timer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            

            keepShooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            
            keepShooting = false;
        }
    }
    private void disparar()
    {
        if (isActive)
        {
            float dist = gameObject.transform.position.x - playerTransform.position.x;


            if (dist < 0)
            {
                if (gameObject.transform.eulerAngles.y == 180)
                {


                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + 180, gameObject.transform.eulerAngles.z);
                }
            }
            if (dist > 0)
            {
                if (gameObject.transform.eulerAngles.y != 180)
                {

                }
                else
                {
                    gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - 180, gameObject.transform.eulerAngles.z);

                }
            }
            // transform.RotateAround(playerTransform.position, Vector3.up, 20000 * Time.deltaTime);
            GameObject bala = Instantiate(balaP, d1.position, d1.rotation);
        }
    }
    /* private void OnCollisionEnter2D(Collision2D collision)
     {
         if (collision.gameObject.name.Equals("disparo_0(Clone)"))
         {
             if (isActive)
             {
                 vidas--;
                 if (vidas <= 0)
                 {
                     isActive = !isActive;
                 }

             }
         }
     }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            vidas--;
            if (vidas <= 0)
            {
                myanimator.SetTrigger("Explosion");
            }
        }
    }
    void Explosion2()
    {
        myanimator.SetBool("Destruido", true);

    }
}

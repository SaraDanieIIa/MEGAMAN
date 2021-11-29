using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Megaman : MonoBehaviour
{
   
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float FuerzaDis;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject disparador;
    [SerializeField] Text reloj;
    [SerializeField] int Vida;
    [SerializeField] GameObject Perder;
    [SerializeField] AudioClip sfx_dash;
    [SerializeField] AudioClip sfx_salto;

    Animator myAnimator;
    Rigidbody2D myBody;
    BoxCollider2D myCollider;

    float animationCoolDown = 0.4f;
    float nextFireBefore;

    float FireRate = 0.2f;
    float nextFireIn;

    bool canDoubleJump = false;

    float dashTime = 0.6f;
    float dashStartTime;
    bool isDashing;

    float Dir = 1;

    float time = 0;
    float count = 0;
    


    // Start is called before the first frame update
    void Start()
    {

        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        StartCoroutine(TimerCorutina());

        Perder.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //TimerNormal();
        MirandoPared();
        dash();
        Mover();
        Saltar();
        Caer();
        Disparar();
        
    }

    bool MirandoPared()
    {
        //return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D raycast_pared = Physics2D.Raycast(myCollider.bounds.center, new Vector2(Dir,0), myCollider.bounds.extents.x + 0.2f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(myCollider.bounds.center, new Vector2(Dir, 0) * (myCollider.bounds.extents.x + 0.1f), Color.green);

        return raycast_pared.collider != null;
    }

    bool estaEnSuelo()
    {
        //return myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        RaycastHit2D colision_suelo = Physics2D.Raycast(myCollider.bounds.center, Vector2.down, myCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));

        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + 0.1f), Color.green);

        return colision_suelo.collider != null;
    }

    void TimerNormal()
    {
        time += Time.deltaTime;
        if(time >= 1f)
        {
            count++;
            reloj.text = count.ToString();
            time = 0;
        }
    }

    IEnumerator TimerCorutina()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            count++;
            reloj.text = count.ToString();
        }
    }

    void Mover()
    {

        float movH = Input.GetAxis("Horizontal");

        
        if(movH != 0)
        {
            
            if(movH < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
            if(MirandoPared() && movH == Dir)
            {
                myAnimator.SetBool("isRunning", false);
                myBody.velocity = new Vector2(0, myBody.velocity.y);
            }
            else
            {
                Dir = transform.localScale.x;
                myAnimator.SetBool("isRunning", true);
                myBody.velocity = new Vector2(movH * speed, myBody.velocity.y);
            }
            

        }
        else
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            myAnimator.SetBool("isRunning", false);
        }
    }

    void Caer()
    {
        if(myBody.velocity.y < -0.05f && !myAnimator.GetBool("Takeof"))
        {
            myAnimator.SetBool("isFalling", true);
            
        }
    }

    void Saltar()
    {
        if(estaEnSuelo())
        {
            if (!myAnimator.GetBool("Takeof"))
            {
                canDoubleJump = false;
                myAnimator.SetBool("isFalling", false);
                myAnimator.SetBool("Takeof", false);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isDashing)
                    {
                        myBody.AddForce(new Vector2(0, jumpForce * 1.2f), ForceMode2D.Impulse);
                    }
                    else
                    {
                        myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                    myAnimator.SetTrigger("salto");
                    myAnimator.SetBool("Takeof", true);
                    AudioSource.PlayClipAtPoint(sfx_salto, Camera.main.transform.position);
                    canDoubleJump = true;
                }
            }
        }
        // este es el doble salto
        if (!estaEnSuelo() && canDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }

    void terminarDeSaltar()
    {
        myAnimator.SetBool("isFalling", true);
        myAnimator.SetBool("Takeof", false);
    }

    void Disparar()
    {
        if(Input.GetKeyDown(KeyCode.X) && Time.time >= nextFireBefore)
        {
            GameObject bala = Instantiate(bullet, disparador.transform.position, disparador.transform.rotation);

            bool direccion = transform.localScale.x == 1;

            (bala.GetComponent<bala>()).shoot(direccion, speed * 2);

            myAnimator.SetLayerWeight(1, 1);

            nextFireBefore = Time.time + animationCoolDown;
            nextFireIn = Time.time + FireRate;
        }
        else
        {
            if (Time.time > nextFireBefore)
                myAnimator.SetLayerWeight(1, 0);
        }
        
    }


    void dash()
    {

        if (estaEnSuelo())
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                dashStartTime = Time.time;
                myAnimator.SetBool("Dash", true);
                isDashing = true;
                AudioSource.PlayClipAtPoint(sfx_dash, Camera.main.transform.position);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                if(Time.time <= dashStartTime + dashTime)
                {
                    myBody.velocity = new Vector2(speed * 2 * transform.localScale.x, myBody.velocity.y);
                   
                }
                else
                {
                    myAnimator.SetBool("Dash", false);
                    isDashing = false;
                }
            }
            else
            {
                myAnimator.SetBool("Dash", false);
                isDashing = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Vida--;
            if(Vida <= 0)
            {
                Perder.SetActive(true);
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vida--;
            if (Vida <= 0)
            {
                Perder.SetActive(true);
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }

}

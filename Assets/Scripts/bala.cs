using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    Rigidbody2D myBody;
    Animator myAnimator;
    [SerializeField] AudioClip sfx_bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //transform.Translate(new Vector3(10 * Time.deltaTime, 0, 0));
    }

    public void shoot(bool dir, float speed)
    {
        myBody = GetComponent<Rigidbody2D>();
        if (dir)
            myBody.velocity = new Vector2(speed, 0);
        else
            myBody.velocity = new Vector2(-speed, 0);

        AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        myAnimator = GetComponent<Animator>();
        myBody.velocity = Vector2.zero;
        myAnimator.SetTrigger("collision");
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

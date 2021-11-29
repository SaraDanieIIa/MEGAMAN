using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ContadorEnemigos : MonoBehaviour
{

    GameObject[] Enemigos;
    public Text TextoContEnem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemigos = GameObject.FindGameObjectsWithTag("Enemigo");

        TextoContEnem.text = "Enemigos: " + Enemigos.Length.ToString();
    }
}

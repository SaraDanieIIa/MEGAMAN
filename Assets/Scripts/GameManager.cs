using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Megaman Player;
    [SerializeField] GameObject GameOverMenuWin;
    [SerializeField] GameObject GameOverMenuLose;
    [SerializeField] int numEnemigos;
    // Start is called before the first frame update
    void Start()
    {
        GameOverMenuWin.SetActive(false);
        GameOverMenuWin.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Kill()
    {
        numEnemigos--;
        if(numEnemigos <= 0)
        {
            Time.timeScale = 0;
            GameOverMenuWin.SetActive(true);
        }
    }

    public void reiniciar_nivel()
    {
        SceneManager.LoadScene(0);
    }
}

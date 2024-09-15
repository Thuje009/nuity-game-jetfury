using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    public void Pause() 
    { 
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("HomePage");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


    public void Close()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

   // public void Sound()
   // {

   // }

   // public void Music()
  //  {
//
   // }




    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWin : MonoBehaviour
{
    [SerializeField] GameObject gameEin;


    public void Hmoe() 
    {
        SceneManager.LoadScene("HomePage");
        Time.timeScale = 1;
    }

    public void Nexts()
    {
        SceneManager.LoadScene("LevelTwo");
        Time.timeScale = 1;
    }

    public void Next1()
    {
        SceneManager.LoadScene("LevelThree");
        Time.timeScale = 1;
    }





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        {
            // ค่อย ๆ ขยายขนาดสเกลจนถึงขนาดเป้าหมาย
        }
    }
}

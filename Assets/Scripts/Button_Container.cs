using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Container : MonoBehaviour
{


    public void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            AsyncOperation asyn = SceneManager.LoadSceneAsync(Random.Range(2, 5));
        }

        else if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            AsyncOperation asyn = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

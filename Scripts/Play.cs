using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
}

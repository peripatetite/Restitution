using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    // Update is called once per frame
    public void PlayGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

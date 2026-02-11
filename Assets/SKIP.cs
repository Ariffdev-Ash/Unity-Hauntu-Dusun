using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SKIP : MonoBehaviour
{

    public void Skip()
    {
        SceneManager.LoadScene(3);
    }
}

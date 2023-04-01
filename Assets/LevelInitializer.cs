using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Oracle;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    public GameObject loadingText;

    public void ExitLevel()
    {
        oracle.Save();
        SceneManager.LoadScene(0);
    }

    public void EnableLevel(int level)
    {
        foreach (var lvl in levels) lvl.SetActive(false);
        switch (level)
        {
            case 1:
                levels[0].SetActive(true);
                break;
            case 2:
                levels[1].SetActive(true);
                break;
            case 3:
                levels[2].SetActive(true);
                break;
            case 4:
                levels[3].SetActive(true);
                break;
            case 5:
                levels[4].SetActive(true);
                break;
            case 6:
                levels[5].SetActive(true);
                break;
            case 7:
                levels[6].SetActive(true);
                break;
            case 8:
                levels[7].SetActive(true);
                break;
            case 9:
                levels[8].SetActive(true);
                break;
            case 10:
                levels[9].SetActive(true);
                break;
            case 11:
                levels[10].SetActive(true);
                break;
            case 12:
                levels[11].SetActive(true);
                break;
        }

        loadingText.SetActive(false);
    }
}
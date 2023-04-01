using System.Collections;
using static Oracle;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private MyButton level1;
    [SerializeField] private MyButton level2;
    [SerializeField] private MyButton level3;
    [SerializeField] private MyButton level4;
    [SerializeField] private MyButton level5;
    [SerializeField] private MyButton level6;
    [SerializeField] private MyButton level7;
    [SerializeField] private MyButton level8;
    [SerializeField] private MyButton level9;
    [SerializeField] private MyButton level10;
    [SerializeField] private MyButton level11;
    [SerializeField] private MyButton level12;

    private void Start()
    {
        level1.onClick.AddListener(() => StartCoroutine(1));
        level2.onClick.AddListener(() => StartCoroutine(2));
        level3.onClick.AddListener(() => StartCoroutine(3));
        level4.onClick.AddListener(() => StartCoroutine(4));
        level5.onClick.AddListener(() => StartCoroutine(5));
        level6.onClick.AddListener(() => StartCoroutine(6));
        level7.onClick.AddListener(() => StartCoroutine(7));
        level8.onClick.AddListener(() => StartCoroutine(8));
        level9.onClick.AddListener(() => StartCoroutine(9));
        level10.onClick.AddListener(() => StartCoroutine(10));
        level11.onClick.AddListener(() => StartCoroutine(11));
        level12.onClick.AddListener(() => StartCoroutine(12));
        SetButtonActiveState();
    }

    public void SetButtonActiveState()
    {
        level2.interactable = oracle.saveData.level1.complete;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;
        level6.interactable = false;
        level7.interactable = false;
        level8.interactable = false;
        level9.interactable = false;
        level10.interactable = false;
        level11.interactable = false;
        level12.interactable = false;
    }

    private void StartCoroutine(int level)
    {
        StartCoroutine(LoadLevel(level));
    }

    private IEnumerator LoadLevel(int level)
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        yield return 0;
        FindObjectOfType<LevelInitializer>().EnableLevel(level);
    }
}
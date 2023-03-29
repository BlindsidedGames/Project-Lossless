
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGridControler : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    
    void Update()
    {
        switch (Screen.width > Screen.height)
        {
            case true:
                grid.constraintCount = 4;
                break;
            default:
                grid.constraintCount = 3;
                break;
        }
    }
}

using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startScreen;
    [SerializeField]
    private GameObject _loseScreen;

    public void ShowStart(){

        _startScreen.SetActive(true);

    }

    public void HideStart(){

        _startScreen.SetActive(false);

    }

    public void ShowLose(){

        _loseScreen.SetActive(true);

    }

    public void HideLose(){

        _loseScreen.SetActive(false);

    }

}

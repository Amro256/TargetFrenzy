using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    #region Main Menu References

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button backButton;
    #endregion


    //Methods
    public void DisplayCreditsMenu()
    {
        mainCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
        creditsButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
    }


    public void HideCreditsMenu()
    {
        creditsCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        creditsButton.gameObject.SetActive(true);
    }
}

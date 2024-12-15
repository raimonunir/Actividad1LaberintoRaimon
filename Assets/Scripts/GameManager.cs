using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject panelStartUI;
    [SerializeField] [Range(1f, 10f)] private float secondsShowingStartMenu;
    [SerializeField] private GameObject panelEndGameUI;
    [SerializeField] private GameObject panelMiniMap;
    [SerializeField] [Range(3f, 6f)] private float secondsToQuitAppAffterWin;

    private Animator panelStartUIanimator;


    // Start is called before the first frame update
    void Start()
    {
        panelEndGameUI.SetActive(false);
        panelStartUIanimator = panelStartUI.GetComponent<Animator>();

        // fade out and disable UI start menu after x seconds
        StartCoroutine(FadeOutStartPanelUI());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            panelMiniMap.SetActive(false);
            panelEndGameUI.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine(ExitAppAfterSeconds(secondsToQuitAppAffterWin));
        }
    }

    private IEnumerator FadeOutStartPanelUI()
    {
        float secondsOffsetAfterAnimationEnds = 1f;

        // fade out start menu
        yield return new WaitForSeconds(secondsShowingStartMenu);
        panelStartUIanimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(panelStartUIanimator.GetCurrentAnimatorStateInfo(0).length + secondsOffsetAfterAnimationEnds);
        // disable start menu
        panelStartUI.SetActive(false);
        panelMiniMap.SetActive(true);
    }

    private IEnumerator ExitAppAfterSeconds(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds); //XXX Realtime is not affected by Time.timeScale
        Debug.Log("QuitApp");
        Application.Quit();
    }
}

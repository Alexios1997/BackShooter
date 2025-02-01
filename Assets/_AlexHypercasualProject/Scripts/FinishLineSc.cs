using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;
using DG.Tweening;

public class FinishLineSc : MonoBehaviour
{
    private Camera currentCam;
    public void Start()
    {
        currentCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }
    public void Win()
    {
        GameControllerSc.Instance.CurrentGameState = Constants.GameState.StartUIAnim;
        PlayerControllerSc.Instance.SetCharacterState(Constants.PlayerState.Win);
        PlayerControllerSc.Instance.Cart.SetActive(false);
        PlayerControllerSc.Instance._characterAnimator.SetBool("Won",true);
        PlayerControllerSc.Instance.GameWonParticle.Play();
        currentCam.gameObject.GetComponent<CameraFollowSc>().offset = new Vector3(10f,10f, currentCam.gameObject.GetComponent<CameraFollowSc>().offset.z);
        StartCoroutine(WinUiShow());
        //Play Camera

        //Play Won Thing UI

    }
    public IEnumerator WinUiShow()
    {
        yield return new WaitForSeconds(1.5f);
        CanvasControllerCs.Instance.WinPanelCanvas();
    }
}

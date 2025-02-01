using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;
using DG.Tweening;

public class PreFinishSc : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Celebrate();
        }
    }
    public void Celebrate()
    {
        GameControllerSc.Instance.CurrentGameState = Constants.GameState.Celebrating;
        EnemyManagerSc.SharedInstance.DefeatAllEnemies();
        foreach (GameObject currentEn in EnemyManagerSc.SharedInstance.EnemyPrefabPooledObjs)
        {
            if (currentEn.activeInHierarchy)
            {
                currentEn.GetComponent<EnemyController>().MoveSpeed = 0f;
                
            }
        }
    }
}

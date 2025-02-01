using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;

public class EnemyManagerSc : MonoBehaviour
{
    //private ObjectPool
    
    
    public static EnemyManagerSc SharedInstance;

    #region Public Vars
    public List<GameObject> EnemyPrefabPooledObjs;
    public GameObject EnemyPrefab;
    public float DistanceFromPlayerInX;
    public int amountToPool;
    public float MaxTimerToSpawn;
    public int HealthToGiveThem;

    #endregion

    #region Private vars

    private GameObject CubeSpawner;
    private GameObject Player;
    private float Timer=0f;
    private float PositionXToSpawn =0f;

    #endregion

    private void Awake()
    {
        SharedInstance = this;

    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PositionXToSpawn = 0f;
        CubeSpawner = GameObject.FindGameObjectWithTag("CubeSpawner");
        EnemyPrefabPooledObjs = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(EnemyPrefab);
            tmp.SetActive(false);
            EnemyPrefabPooledObjs.Add(tmp);
        }
    }

    private void Update()
    {
        if(GameControllerSc.Instance.CurrentGameState == Constants.GameState.Playing)
        {

            Timer += Time.deltaTime;
           // Debug.Log("Timer: " + Timer);

            if (Timer >= MaxTimerToSpawn)
            {
              

                GameObject Enemy = GetPooledObjs();
                if (Enemy != null)
                {
                    PositionXToSpawn = Player.transform.position.x - SharedInstance.DistanceFromPlayerInX;
                    float RandomNumPositionZ = Random.Range(-(CubeSpawner.transform.localScale.z), CubeSpawner.transform.localScale.z);                    
                    Enemy.transform.position = new Vector3(PositionXToSpawn, CubeSpawner.transform.position.y, RandomNumPositionZ);
                    Enemy.GetComponent<EnemyController>().health = HealthToGiveThem;
                    Enemy.SetActive(true);
                }

                Timer =0f;

            }

        }
    }
    public GameObject GetPooledObjs()
    {
        for (int i=0;i<amountToPool;i++)
        {
            if (!EnemyPrefabPooledObjs[i].activeInHierarchy)
            {
                return EnemyPrefabPooledObjs[i];
            }
        }
        return null;
    }
    public void LaughAllEnemies()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (EnemyPrefabPooledObjs[i].activeInHierarchy)
            {
                EnemyPrefabPooledObjs[i].GetComponent<EnemyController>()._EnemyAnimator.SetBool("Won",true);
            }
        }
        
    }
    public void DefeatAllEnemies()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (EnemyPrefabPooledObjs[i].activeInHierarchy)
            {
                EnemyPrefabPooledObjs[i].GetComponent<EnemyController>()._EnemyAnimator.SetBool("Lost", true);
            }
        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;
using DG.Tweening;
public class WeaponScript : MonoBehaviour
{
    public List<GameObject> BulletPooledObjs;
    public GameObject BulletPrefab;
    public GameObject PointToShoot;
    public Animation WeaponAnimationShoot;
    public ParticleSystem WeaponFlash;
    public GameObject TextM;
    public int amountToPool;
    public float RateOfFire;
    public float LifeTime = 5f;
    public float BulletSpeed = 15f;
    public int MaxBullets;
    public int InitialBullets;
    public int CurrentBullets;
    public int DamageToGive;

    private float TimerToSpawn = 0f;


    private void OnEnable()
    {
        CurrentBullets = InitialBullets;

        BulletPooledObjs = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(BulletPrefab);
            tmp.SetActive(false);
            BulletPooledObjs.Add(tmp);
        }
        TextM.GetComponent<TextMesh>().text = CurrentBullets.ToString() + "/" + MaxBullets.ToString();
    }


    void Update()
    {

        if (GameControllerSc.Instance.CurrentGameState == Constants.GameState.Playing)
        {



            if (CurrentBullets < MaxBullets && CurrentBullets>0)
            {
                TimerToSpawn += Time.deltaTime;
                // Debug.Log("Timer: " + Timer);

                if (TimerToSpawn >= RateOfFire)
                {


                    GameObject Bullet = GetPooledObjs();
                    if (Bullet != null)
                    {

                        Bullet.GetComponent<BulletController>()._BulletLifeTime = LifeTime;
                        Bullet.GetComponent<BulletController>()._BulletSpeed = BulletSpeed;
                        Bullet.GetComponent<BulletController>().DamageTaken = DamageToGive;
                        Bullet.transform.position = new Vector3(PointToShoot.transform.position.x, PointToShoot.transform.position.y, PointToShoot.transform.position.z);
                        Bullet.SetActive(true);
                        CurrentBullets -= 1;
                        WeaponAnimationShoot.Play();
                        WeaponFlash.Play();
                    }

                    TimerToSpawn = 0f;

                }
            }
            
            else if (CurrentBullets >= MaxBullets)
            {
                Debug.Log("CHANGE WEAPON");
                PlayerControllerSc.Instance.ChangeWeapon();
            }

            TextM.GetComponent<TextMesh>().text = CurrentBullets.ToString() + "/" + MaxBullets.ToString();


        }

    }
    public GameObject GetPooledObjs()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!BulletPooledObjs[i].activeInHierarchy)
            {
                return BulletPooledObjs[i];
            }
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _BulletSpeed;
    public float _BulletLifeTime;

    private float TimerToSpawn = 0f;
    public int DamageTaken;
    public ParticleSystem HitParticle;
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(-(_BulletSpeed * Time.deltaTime),0f, 0f);
        TimerToSpawn += Time.deltaTime;
        if (TimerToSpawn >= _BulletLifeTime)
        {
            TimerToSpawn = 0f;
            gameObject.SetActive(false);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            HitParticle.transform.position = new Vector3(transform.position.x, HitParticle.transform.position.y, transform.position.z);
            HitParticle.Play();
            HitParticle.transform.parent = null;
            other.gameObject.GetComponent<EnemyController>().Damage(DamageTaken,0.5f);
            TimerToSpawn = 0f;
            gameObject.SetActive(false);
            
            
        }
    }
    
}

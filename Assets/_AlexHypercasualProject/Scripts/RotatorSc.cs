using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class RotatorSc : MonoBehaviour
{

    public float duration;
    public Vector3 Rotation;
    public RotateMode mode;
    public LoopType currentLoop;
    public ParticleSystem CoinParticles;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.DORotate(Rotation, duration).SetLoops(-1, currentLoop);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CoinParticles.transform.position = this.gameObject.transform.position;
            CoinParticles.Play();
            CoinParticles.transform.parent = null;
            CanvasControllerCs.Instance.CoinNumber++;
            Debug.Log(CanvasControllerCs.Instance.CoinNumber);
            gameObject.SetActive(false);
        }
    }

}

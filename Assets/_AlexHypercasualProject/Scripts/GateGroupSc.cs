using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateGroupSc : MonoBehaviour
{
    public GameObject GateLeft;
    public GameObject GateRight;

    // Update is called once per frame
    void Update()
    {
        if (!GateLeft.activeInHierarchy)
        {

            GateRight.SetActive(false);
            StartCoroutine(DisableAfterSeconds());
        }
        if (!GateRight.activeInHierarchy)
        {
            GateLeft.SetActive(false);
            StartCoroutine(DisableAfterSeconds());
        }
    }
    public IEnumerator DisableAfterSeconds()
    {
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(false);
    }
}

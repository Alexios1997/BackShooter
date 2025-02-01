using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexHyperCasualGames;
using DG.Tweening;
using TMPro;
using System;

public class GateScript : MonoBehaviour
{

    public int NumberToHandle;
    public Constants.Operations CurrentOperation;
    public GameObject TextGui;
    public ParticleSystem CurrentParticles;

    public void Start()
    {
        string CurrentOper = GetCurrentOperationString();
        TextGui.GetComponent<TextMeshPro>().text = "x " + CurrentOper + " " + NumberToHandle.ToString();
    }

    private string GetCurrentOperationString()
    {
        switch (CurrentOperation)
        {
            case Constants.Operations.Add:
                return "+";
                
            case Constants.Operations.Minus:
                return "-";
                
            case Constants.Operations.Multiply:
                return "*";
                
            case Constants.Operations.Divide:
                return "/";
            default:
                return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CurrentParticles.transform.position = new Vector3(other.transform.position.x, (other.transform.position.y+1f), other.transform.position.z);
            CurrentParticles.Play();
            CurrentParticles.transform.parent = null;
            DoOperation();
        }
    }

    private void DoOperation()
    {
        
        switch (CurrentOperation)
        {
            case Constants.Operations.Add:
                PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets += NumberToHandle;
                break;

            case Constants.Operations.Minus:
                PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets -= NumberToHandle;
                break;
                
            case Constants.Operations.Multiply:
                PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets = Mathf.RoundToInt(PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets * NumberToHandle);
                break;

            case Constants.Operations.Divide:
                PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets = Mathf.RoundToInt(PlayerControllerSc.Instance.EquippedWeapon.GetComponent<WeaponScript>().CurrentBullets / NumberToHandle);
                break;
            default:
                break;
                
        }
        gameObject.SetActive(false);
    }
}

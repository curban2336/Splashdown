using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> creditsUI;
    [SerializeField] List<GameObject> intructionsUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(false);
        }
        foreach (GameObject instruction in intructionsUI)
        {
            instruction.SetActive(false);
        }
    }

    public void ActivateCredits()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(true);
        }
    }

    public void ActivateInstruct()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(true);
        }
    }

    public void ReturnToMenu()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(false);
        }
        foreach (GameObject instruction in intructionsUI)
        {
            instruction.SetActive(false);
        }
    }
}

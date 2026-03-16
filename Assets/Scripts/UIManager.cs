using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<GameObject> creditsUI;
    [SerializeField] List<GameObject> instructionsUI;
    [SerializeField] List<GameObject> mainMenuUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(false);
        }
        foreach (GameObject instruction in instructionsUI)
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
        foreach(GameObject main in mainMenuUI)
        {
            main.SetActive(false);
        }
    }

    public void ActivateInstruct()
    {
        foreach (GameObject instruction in instructionsUI)
        {
            instruction.SetActive(true);
        }
        foreach (GameObject main in mainMenuUI)
        {
            main.SetActive(false);
        }
    }

    public void ReturnToMenu()
    {
        foreach (GameObject credit in creditsUI)
        {
            credit.SetActive(false);
        }
        foreach (GameObject instruction in instructionsUI)
        {
            instruction.SetActive(false);
        }
        foreach (GameObject main in mainMenuUI)
        {
            main.SetActive(true);
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MineSpawn : MonoBehaviour
{
    private Vector3 MineHeight;
    private int RollHeightOne;
    private int RollHeightTwo;
    private bool MineNumCurrent = false;
    [SerializeField] GameObject MineOne;
    [SerializeField] GameObject MineTwo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //int RollHeightOne = Random.Range(1, 4);
        //int RollHeightTwo = Random.Range(1, 4);
    }

    public void MineReachEnd()
    {
        Debug.Log("Reach End");
        //int RollHeightOne = Random.Range(1, 4);
        //int RollHeightTwo = Random.Range(1, 4);
        //Debug.Log(RollHeightOne);
        //Debug.Log(RollHeightTwo);
        //gameObject.SendMessage("RollHeightOneMove", RollHeightOne);
        //gameObject.SendMessage("RollHeightTwoMove", RollHeightTwo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

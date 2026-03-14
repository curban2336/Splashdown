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
        int RollHeightOne = Random.Range(1, 3);
        int RollHeightTwo = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

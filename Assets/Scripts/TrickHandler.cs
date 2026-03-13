using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TrickHandler : MonoBehaviour
{
    [System.Serializable]
    public class serializableClass
    {
        public List<GameObject> arrowList;
    }
    public List<serializableClass> slots = new List<serializableClass>();
    [SerializeField] List<GameObject> trickSeq;
    [SerializeField] Color defaultColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color wrongColor;
    //[SerializeField] WaterMovement pMovement;
    [SerializeField] ColorSwitcherWater water;

    [SerializeField] bool trickTime = false;
    [SerializeField] bool setTrick = false;
    public int trickCount = 3;
    public int trickIndex = 0;
    public float trickTimeLimit;
    public float trickTimeLimitStart;

    [SerializeField] public static int wrongCount = 0;
    [SerializeField] BG_MoveLeft background;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void ResetTrick()
    {
        trickIndex = 0;
        trickTimeLimit = trickTimeLimitStart;
        StartCoroutine("Wait");
    }

    void NextTrick()
    {
        trickIndex = 8;
        trickTimeLimit = trickTimeLimitStart;
        for (int i = 0; i < trickSeq.Count; i++)
        {
            trickSeq[i].GetComponent<SpriteRenderer>().color = defaultColor;
            trickSeq[i].GetComponent<SpriteRenderer>().enabled = false;
        }
        setTrick = false;
        trickSeq.Clear();
        wrongCount++;
    }

    public void ActivateTrickTime()
    {
        int trickNum = 3 + Mathf.FloorToInt((background.speed - 10) / 4);
        if (background.speed <= 10)
        {
            trickNum = 3;
        }
        water.jumpForce = 20f + (10 * Mathf.FloorToInt((trickNum - 3) / 4));
        if (background.speed <= 10)
        {
            water.jumpForce = 20f;
        }
        trickCount = trickNum;
        trickTime = true;
        trickTimeLimit = 360f;
        trickTimeLimitStart = 360f;
    }

    IEnumerator Wait()
    {
        for (int i = 0; i < trickSeq.Count; i++)
        {
            trickSeq[i].GetComponent<SpriteRenderer>().color = defaultColor;
        }
        yield return new WaitForSeconds(0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (trickTime)
        {
            if(!setTrick)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    int randomIndex = Random.Range(0, 4);
                    slots[i].arrowList[randomIndex].GetComponent<SpriteRenderer>().enabled = true;
                    trickSeq.Add(slots[i].arrowList[randomIndex]);
                }
                WaterMovement.isInWater = false;
                setTrick = true;
            }
            //trickTimeLimit -= Time.deltaTime;
            if (trickCount == 0)
            {
                ResetTrick();
                trickTime = false;
                trickTimeLimit = trickTimeLimitStart;
                foreach (GameObject obj in trickSeq)
                {
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                }
                trickSeq.Clear();
                trickIndex = 0;
                setTrick = false;
                WaterMovement.isInWater = true;
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.W) && trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if(Input.GetKeyDown(KeyCode.W) && !trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.S) && trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.S) && !trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.D) && trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.D) && !trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.A) && trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.A) && !trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                    NextTrick();
                }
            }
        }

        if (trickTime && trickIndex >= trickSeq.Count)
        {
            trickIndex = 0;
            trickTimeLimit = trickTimeLimitStart;
            for (int i = 0; i < trickSeq.Count; i++)
            {
                trickSeq[i].GetComponent<SpriteRenderer>().color = defaultColor;
                trickSeq[i].GetComponent<SpriteRenderer>().enabled = false;
            }
            --trickCount;
            setTrick = false;
            trickSeq.Clear();
        }

        if (!ColorSwitcherWater.isJumping)
        {
            trickTime = false;
            trickTimeLimit = trickTimeLimitStart;
            foreach (GameObject obj in trickSeq)
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
                obj.GetComponent<SpriteRenderer>().color = defaultColor;
            }
            trickSeq.Clear();
            trickIndex = 0;
            setTrick = false;
            WaterMovement.isInWater = true;
            wrongCount = 0;
        }
    }
}

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
    [SerializeField] List<Sprite> correctSprites; // 0 = up, 1 = down, 2 = right, 3 = left
    [SerializeField] List<Sprite> wrongSprites; // 0 = up, 1 = down, 2 = right, 3 = left
    [SerializeField] List<Sprite> defaultSprites; // 0 = up, 1 = down, 2 = right, 3 = left
    [SerializeField] WaterMovement pMovement;
    [SerializeField] public ColorSwitcherWater water;

    [SerializeField] bool trickTime = false;
    [SerializeField] bool setTrick = false;
    public int trickCount = 3;
    public int trickIndex = 0;
    public float trickTimeLimit;
    public float trickTimeLimitStart;

    [SerializeField] public static int wrongCount = 0;
    [SerializeField] public BG_MoveLeft background;

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
        ++wrongCount;
        trickIndex = 8;
        trickTimeLimit = trickTimeLimitStart;
        foreach (GameObject obj in trickSeq)
        {
            if (obj.CompareTag("up"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
            }
            else if (obj.CompareTag("down"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
            }
            else if (obj.CompareTag("right"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
            }
            else if (obj.CompareTag("left"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
            }
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
        setTrick = false;
        trickSeq.Clear();
        Debug.Log("Wrong Count: " + wrongCount);
    }

    public void ActivateTrickTime()
    {
        int trickNum = 3 + Mathf.FloorToInt((background.speed - 10) / 4);
        if (background.speed <= 10)
        {
            trickNum = 3;
        }
        trickCount = trickNum;
        ScoreReporting.totalTricks = trickNum;
        trickTime = true;
        trickTimeLimit = 360f;
        trickTimeLimitStart = 360f;
    }

    IEnumerator Wait()
    {
        foreach (GameObject obj in trickSeq)
        {
            if (obj.CompareTag("up"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
            }
            else if (obj.CompareTag("down"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
            }
            else if (obj.CompareTag("right"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
            }
            else if (obj.CompareTag("left"))
            {
                obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
            }
            //obj.GetComponent<SpriteRenderer>().enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (trickTime)
        {
            if (!setTrick)
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
                if (Input.GetKeyDown(KeyCode.W) && trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = correctSprites[0];
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.W) && !trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.S) && trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = correctSprites[1];
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.S) && !trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.D) && trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = correctSprites[2];
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.D) && !trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
                    NextTrick();
                }
                if (Input.GetKeyDown(KeyCode.A) && trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = correctSprites[3];
                    trickIndex++;
                }
                else if (Input.GetKeyDown(KeyCode.A) && !trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
                    NextTrick();
                }
            }
        }

        if (trickTime && trickIndex >= trickSeq.Count)
        {
            trickIndex = 0;
            trickTimeLimit = trickTimeLimitStart;
            foreach (GameObject obj in trickSeq)
            {
                if (obj.CompareTag("up"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
                }
                else if (obj.CompareTag("down"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
                }
                else if (obj.CompareTag("right"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
                }
                else if (obj.CompareTag("left"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
                }
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
            --trickCount;
            setTrick = false;
            trickSeq.Clear();
            pMovement.StartCoroutine("TrickCoroutine");
        }
        else if (trickTime && trickIndex < trickSeq.Count && !ColorSwitcherWater.isJumping)
        {
            trickIndex = 0;
            trickTimeLimit = trickTimeLimitStart;
            foreach (GameObject obj in trickSeq)
            {
                if (obj.CompareTag("up"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
                }
                else if (obj.CompareTag("down"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
                }
                else if (obj.CompareTag("right"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
                }
                else if (obj.CompareTag("left"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
                }
            }
            --trickCount;
            ++wrongCount;
            pMovement.StopCoroutine("TrickCoroutine");
            setTrick = false;
            trickSeq.Clear();

            foreach(serializableClass slot in slots)
            {
                foreach (GameObject obj in slot.arrowList)
                {
                    if (obj.CompareTag("up"))
                    {
                        obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
                    }
                    else if (obj.CompareTag("down"))
                    {
                        obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
                    }
                    else if (obj.CompareTag("right"))
                    {
                        obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
                    }
                    else if (obj.CompareTag("left"))
                    {
                        obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
                    }
                    obj.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }

        if (!ColorSwitcherWater.isJumping)
        {
            trickTime = false;
            trickTimeLimit = trickTimeLimitStart;
            foreach (GameObject obj in trickSeq)
            {
                if (obj.CompareTag("up"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[0];
                }
                else if (obj.CompareTag("down"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[1];
                }
                else if (obj.CompareTag("right"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[2];
                }
                else if (obj.CompareTag("left"))
                {
                    obj.GetComponent<SpriteRenderer>().sprite = defaultSprites[3];
                }
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
            trickSeq.Clear();
            trickIndex = 0;
            setTrick = false;
            WaterMovement.isInWater = true;
            //wrongCount = 0;
        }
    }
}
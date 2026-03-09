using System.Collections.Generic;
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

    [SerializeField] bool trickTime = true;
    [SerializeField] bool setTrick = false;
    public int trickCount = 1;
    public int trickIndex = 0;
    public float trickTimeLimit = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(trickTime)
        {
            if(!setTrick)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    int randomIndex = Random.Range(0, 4);
                    slots[i].arrowList[randomIndex].GetComponent<SpriteRenderer>().enabled = true;
                    trickSeq.Add(slots[i].arrowList[randomIndex]);
                }
                setTrick = true;
            }
            trickTimeLimit -= Time.deltaTime;
            if (trickTimeLimit <= 0)
            {
                trickTime = false;
                //setTrick = false;
                trickTimeLimit = 3f;
                trickSeq.Clear();
                trickIndex = 0;
            }
            else
            {
                if(Input.GetKey(KeyCode.W) && trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if(Input.GetKey(KeyCode.W) && !trickSeq[trickIndex].CompareTag("up"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                }
                if (Input.GetKey(KeyCode.S) && trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKey(KeyCode.S) && !trickSeq[trickIndex].CompareTag("down"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                }
                if (Input.GetKey(KeyCode.D) && trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKey(KeyCode.D) && !trickSeq[trickIndex].CompareTag("right"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                }
                if (Input.GetKey(KeyCode.A) && trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = correctColor;
                    trickIndex++;
                }
                else if (Input.GetKey(KeyCode.A) && !trickSeq[trickIndex].CompareTag("left"))
                {
                    trickSeq[trickIndex].GetComponent<SpriteRenderer>().color = wrongColor;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static List<string> stuffName = new List<string>();    
    public static int holdNumber;
    public static string holdItem;
    public static bool isHold;

    public GameObject item;
    public GameObject hand;

    [Header("Food")]
    public GameObject apple;
    public GameObject cookie;
    public GameObject water;
    public GameObject pie;

    [Header("Icon")]
    public GameObject appleIcon;
    public GameObject cookieIcon;
    public GameObject waterIcon;
    public GameObject pieIcon;
    public Transform[] place = new Transform[6];
    void Start()
    {
        holdNumber = 0;
        isHold = false;

        getItem("water");
    }


    void Update()
    {
        hold();
        if (stuffName.Count < holdNumber) holdNumber = 0;
    }

    public void getItem(string name)
    {
        stuffName.Add(name);
        setItem();
    }

    public void setItem()
    {
        for (int i = 0; i < 6; i++)
        {
            if (place[i].GetChildCount() != 0) Destroy(place[i].GetChild(0).gameObject);
        }
        for(int i=0; i < stuffName.Count; i++)
        {
            if (stuffName[i] == "apple") Instantiate(appleIcon, place[i]);
            if (stuffName[i] == "cookie") Instantiate(cookieIcon, place[i]);
            if (stuffName[i] == "water") Instantiate(waterIcon, place[i]);
            if (stuffName[i] == "pie") Instantiate(pieIcon, place[i]);
        }
    }

    void hold()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) changedKey(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) changedKey(2);
        if (Input.GetKeyDown(KeyCode.Alpha3)) changedKey(3);
        if (Input.GetKeyDown(KeyCode.Alpha4)) changedKey(4);
        if (Input.GetKeyDown(KeyCode.Alpha5)) changedKey(5);
        if (Input.GetKeyDown(KeyCode.Alpha6)) changedKey(6);

        if (holdNumber == 0 || stuffName.Count < holdNumber) resetHand();
        else if (stuffName.Count >= holdNumber && !isHold)
        {
            if (stuffName[holdNumber - 1] == "apple") Instantiate(apple, item.transform);
            if (stuffName[holdNumber - 1] == "cookie") Instantiate(cookie, item.transform);
            if (stuffName[holdNumber - 1] == "water") Instantiate(water, item.transform);
            if (stuffName[holdNumber - 1] == "pie") Instantiate(pie, item.transform);
            // 아이템 들기

            holdItem = stuffName[holdNumber - 1];
            hand.SetActive(true);
            isHold = true;
            
        }
    }
    void resetHand()
    {
        if (item.transform.GetChildCount() != 0) Destroy(item.transform.GetChild(0).gameObject);
        hand.SetActive(false);
    }

    void changedKey(int num)
    {
        resetHand();
        isHold = false;
        if (holdNumber == num)
        {
            holdNumber = 0;
            return;
        }
        holdNumber = num;
    }
}

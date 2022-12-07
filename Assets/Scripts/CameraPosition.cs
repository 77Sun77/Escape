using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public Character player;
    public GameObject ui;
    public Transform head;

    public Transform[] position;

    int changeoverNumber;

    public bool isChanged;
    void Awake()
    {
        changeoverNumber = 0;
        transform.parent = position[0];
        isChanged = true;
    }

    void Update()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        if (Input.GetMouseButtonDown(0) && isChanged)
        {
            if(changeoverNumber == 0)
            {
                Invoke("returnCamera", 3f);
                changeoverNumber++;
            }
        }
    }

    public void returnCamera() // 카메라를 플레이어한테 전환
    {
        if (!player.isActiveAndEnabled)
        {
            player.gameObject.SetActive(true);
            ui.SetActive(true);
        }
        transform.parent = head;
        isChanged = false;
    }

    IEnumerator changeoverTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

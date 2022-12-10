using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform Player;

    float y_Temp;
    Vector3 curTemp;
    bool isOpen;

    private void Start()
    {
        isOpen = false;
    }
    void Update()
    {
        Quaternion temp = Quaternion.Euler(Vector3.up * y_Temp);
        transform.rotation = Quaternion.Lerp(transform.rotation, temp, Time.deltaTime * 3);
    }

    public void changedIsOpen()
    {
        isOpen = !isOpen;
        print("Change");
        if (isOpen)
        {
            if(Player.position.z < transform.position.z)
            {
                y_Temp = 90;
            }
            else
            {
                y_Temp = -90;
            }
        }
        else
        {
            y_Temp = 0;
        }
    }
}

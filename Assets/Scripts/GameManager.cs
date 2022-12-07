using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CameraPosition cameraPosition;
    public Slider hpBar;
    public int hp;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;
        if (hp > 100) hp = 100;
    }

}

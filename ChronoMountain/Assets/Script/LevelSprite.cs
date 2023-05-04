using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSprite : MonoBehaviour
{
    public static LevelSprite manager;

    public Sprite wall;
    public Sprite path;
    public Sprite bumper;
    public Sprite end;
    public Sprite hole;

    void Awake()
    {
        manager = this;
    }
}

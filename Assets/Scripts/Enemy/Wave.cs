﻿using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _count;

    public Enemy Enemy => _enemy;
    public int Count => _count;
}
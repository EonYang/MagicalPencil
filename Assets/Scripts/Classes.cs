using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int Id;
    public string AnA = "";
    public string Name;

    public string Tag1;
    public string Tag2 = "";

    public bool Prerequisite = false;

    public int SizeH = 0;
    public int RecoverHP = 0;
    public int Attack = 0;
    public int DemageToPlayer = 0;
	public int Speed = 100;
	public int Durability = 1;
	
    public bool CanPickUp = false;
    public string ActionInBagName = "";
    public string ActionInBagFunction = "";

    public string ActionOnGroundName = "";
    public string ActionOnGroundFunction = "";

    public bool AutoTriggered = false;
    public string ActionAutoTriggerFunction = "";

    public string StoryOnCreate = "";
    public string StoryOnUse = "";
    public string StoryOnPickUp = "";

    public static int itemsCount = 0;

	public Sprite sprite;
   
    public Item()
    {
		itemsCount++;
    }
}

[System.Serializable]
public class Items
{
    public Item[] items;
}

[System.Serializable]
public class PuzzleSolver
{
    public int Id;
	public string Name;
	public int Result;
	public string ResultText;
	public string NextLevel;
}

[System.Serializable]
public class PuzzleAndItsSolvers
{
    public int Id;
	public List<PuzzleSolver> Solvers = new List<PuzzleSolver>();
}

[System.Serializable]
public class PredictionData
{
    public string[] names;
    public int[] numbers;
}

[System.Serializable]
public class PredictionResponse
{
    public PredictionData prediction;
}

[System.Serializable]
public class SpriteNameResponse
{
	public string fileName;
}


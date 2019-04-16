using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Based_Settings : MonoBehaviour {

    public static Scene_Based_Settings Instance;

    [SerializeField]
    public Color UIcolor = Color.white;

    [SerializeField]
    public int[] PuzzleIDs = new int[1] { 1 };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

}

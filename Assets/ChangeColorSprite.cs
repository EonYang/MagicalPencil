using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorSprite : MonoBehaviour {

    // Reference to Sprite Renderer component
    private SpriteRenderer rend;

    // Color value that we can set in Inspector
    // It's White by default

    // Use this for initialization
    private void Start()
    {
        Color colorToTurnTo = Scene_Based_Settings.Instance.UIcolor;

        // Assign Renderer component to rend variable
        rend = gameObject.GetComponent<SpriteRenderer>();

        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
    }



}

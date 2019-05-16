using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideShowManager : MonoBehaviour
{

    // Use this for initialization



    [SerializeField]
    private List<Sprite> slides = new List<Sprite>();
    private string nextScene = "Level_Beginning";

    private Image imageRenderer;

    private int currentSlideIndex = 0;

    void Start()
    {
        imageRenderer = GetComponent<Image>();
        imageRenderer.sprite = slides[currentSlideIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentSlideIndex < slides.Count - 1)
            {
                currentSlideIndex += 1;
                StartCoroutine(ChangeSlide(currentSlideIndex));

            }
            else
            {
                StartGame();
            }

        }
    }

    IEnumerator ChangeSlide(int index)
    {
        float t = 0.6f;
        imageRenderer.CrossFadeAlpha(0, t, false);
        yield return new WaitForSeconds(t);
        imageRenderer.sprite = slides[currentSlideIndex];
        imageRenderer.CrossFadeAlpha(1, t, false);
    }

    //void OnMouseDown()
    //{
    //    imageRenderer.CrossFadeAlpha(0, 1, false);
    //    imageRenderer.sprite = slides[currentSlideIndex];
    //    imageRenderer.CrossFadeAlpha(1, 1, false);
    //}

    public void StartGame()
    {
         SceneManager.LoadScene(nextScene);
    }
}
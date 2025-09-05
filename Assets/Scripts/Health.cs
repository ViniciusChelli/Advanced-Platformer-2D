using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int heartsCount;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite noHeart;

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = noHeart;
            }


            if(i < heartsCount)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}

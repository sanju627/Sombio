using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SRV_Panel : MonoBehaviour
{
    public SurvivalShop srvShop;
    public GameObject[] panels;
    public Image[] selectorPanels;
    [Space]
    public Sprite disableSprite;
    public Sprite eableSprite;

    // Start is called before the first frame update
    void Start()
    {
        SelectSRV(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSRV(int index)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        srvShop.index = index;
        srvShop.DisableCustom();

        for (int i = 0; i < selectorPanels.Length; i++)
        {
            selectorPanels[i].sprite = disableSprite;
        }

        

        panels[index].SetActive(true);
        selectorPanels[index].sprite = eableSprite;
    }
}

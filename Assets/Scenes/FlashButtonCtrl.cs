using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashButtonCtrl : MonoBehaviour
{
    [SerializeField]
    Image SPR_FLASH;
    static float CNT_MAX = 40;
    float count = -1;
    // Start is called before the first frame update
    void Start()
    {
        SPR_FLASH.enabled = false;
        count = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (count >= 0)
        {
            SPR_FLASH.enabled = true;
            SPR_FLASH.color = new Color(1.0f, 1.0f, 0.5f, (CNT_MAX - count) / CNT_MAX);
            SPR_FLASH.transform.localScale = new Vector3(0.1f * count, 0.1f * count, 1);
            count++;
            if (count >= CNT_MAX)
            {
                SPR_FLASH.enabled = false;
                count = -1;
			}
		}
    }

    public void Flash()
    {
        count = 0;
	}
}

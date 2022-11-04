using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIPanel : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Tony tony;
    public Sprite healthEmpty;
    public Sprite healthHalf;
    public Sprite healthFull;

    Text        keyCountText;
    public List<Image> healthImages;

    // Start is called before the first frame update
    void Start()
    {
        tony = GameObject.Find("Tony").GetComponent<Tony>();
        //钥匙的计数
        Transform keyTrans = transform.Find("Key Count");
        keyCountText = keyTrans.GetComponent<Text>();

        //生命值图标
        Transform healthPanel = transform.Find("Health Panel");
        healthImages = new List<Image>();
        if (healthPanel!= null) 
        {
            for (int i = 0; i < 20; i++)
            {
                Transform trans = healthPanel.Find("H_" + i);
                if (trans==null) 
                {
                    break;
                }
                healthImages.Add(trans.GetComponent<Image>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //显示钥匙数量
        keyCountText.text = tony.keyCount.ToString();


        //显示生命数量
        int health = tony.health;
        //Debug.Log(healthImages.Count);

        for (int i = 0; i < healthImages.Count; i++)
        {
            if (health > 1)
            {
                healthImages[i].sprite = healthFull;
            }
            else if (health == 1)
            {
                healthImages[i].sprite = healthHalf;
            }
            else 
            {
                healthImages[i].sprite = healthEmpty;
            }
            //2等于一颗星星
            health -= 2;
        }
    }
}

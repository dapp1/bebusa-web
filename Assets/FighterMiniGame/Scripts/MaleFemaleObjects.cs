using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleFemaleObjects : MonoBehaviour
{
    public int weapon;
    void Start()
    {
        if (PlayerPrefs.GetInt("Skin") == 10 || PlayerPrefs.GetInt("Skin") == 11)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            if (weapon == 1)
            {
                
                gameObject.transform.localPosition = new Vector3(-0.025f, -0.029f, -0.033f);
                gameObject.transform.localRotation = Quaternion.Euler(-180f, -10f, 343.933f);
            }

            else
            {
                gameObject.transform.localPosition = new Vector3(-0.216f, 0f, -0.085f);
                gameObject.transform.localRotation = Quaternion.Euler(-191.5f, 0f, 0f);
            }
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SumController : MonoBehaviour {
    private InputField myInputField;

    void Start()
    {
        myInputField = gameObject.GetComponent<InputField>();
    }

    public void CalcSum()
    {
        int? sum1 = 0, sum2 = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sum1 += MagicSquare4Manager.I.MsCells[i + j * 4];
                sum2 += MagicSquare4Manager.I.MsCells[j + i * 4];
            }
            if(sum1 != null || sum2 != null)
            {
                myInputField.text = (sum1 == null) ? sum2.ToString() : sum1.ToString();
                return;
            }
        }

        sum1 = 0; sum2 = 0;
        for(int i = 0; i < 4; i++)
        {
            sum1 += MagicSquare4Manager.I.MsCells[i + i * 4];
            sum2 += MagicSquare4Manager.I.MsCells[3 - i + i * 4];
        }
        if (sum1 != null || sum2 != null)
        {
            myInputField.text = (sum1 == null) ? sum2.ToString() : sum1.ToString();
            return;
        }
    }
}

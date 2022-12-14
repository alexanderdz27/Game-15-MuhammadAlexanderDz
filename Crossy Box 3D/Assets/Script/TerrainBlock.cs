using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBlock : MonoBehaviour
{
    [SerializeField] GameObject Main;
    [SerializeField] GameObject Repeat;

    private int extent;

    public int Extent { get => extent;}

    public void Build(int extent)
    {
        this.extent = extent;

        for (int i = -1; i <= 1; i++)
        {
            if (i == 0)
            {
                continue;
            }
            var m = Instantiate(Main);
            m.transform.SetParent(this.transform);
            m.transform.localPosition = new Vector3((extent+1)*i,0,0);
            m.GetComponentInChildren<Renderer>().material.color *= Color.gray;
        }


        Main.transform.localScale = new Vector3(extent*2+1, Main.transform.localScale.y, Main.transform.localScale.z);
        
        if (Repeat==null)
        {
            return;
        }
        
        for (int x = -(extent+1); x <= extent+1; x++)
        {
            if (x == 0)
            {
                continue;
            }

            var r = Instantiate(Repeat);
            r.transform.SetParent(this.transform);
            r.transform.localPosition = new Vector3(x,0,0);
        }
    }
}

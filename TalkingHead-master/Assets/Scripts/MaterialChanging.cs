using UnityEngine;
using System.Collections;

public class MaterialChanging : MonoBehaviour {
    private Material rend;
    public Color defaultColor;
	// Use this for initialization
	void Start () {
        rend = this.GetComponent<Renderer>().material;
        defaultColor=rend.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void lowAlpha()
	{
        Color col=rend.GetColor("_Color");
        col.a = 1f;
        ApplyTransparent(col);
	}
	public void changeMaterialtoDefaultA()
	{
        Color col=defaultColor;
        ApplyTransparent(new Color(col.r, col.g, col.b, 0.5f));
	}

    void ApplyTransparent(Color col)
    {
        rend = this.gameObject.GetComponent<Renderer>().material;
        rend.SetFloat("_Mode", 3f);
        rend.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        rend.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        rend.SetInt("_ZWrite", 0);
        rend.DisableKeyword("_ALPHATEST_ON");
        rend.DisableKeyword("_ALPHABLEND_ON");
        rend.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        rend.renderQueue = 3000;
        StartCoroutine(colorLerp1(rend.GetColor("_Color"), col));
    }
    public void ApplyOpaque(Color col)
    {
        rend = this.gameObject.GetComponent<Renderer>().material;
        rend.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        rend.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        rend.SetInt("_ZWrite", 1);
        rend.DisableKeyword("_ALPHATEST_ON");
        rend.DisableKeyword("_ALPHABLEND_ON");
        rend.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        rend.renderQueue = -1;
        StartCoroutine(colorLerp1(rend.GetColor("_Color"), col));
    }
    IEnumerator colorLerp1(Color col1, Color col2)
    {
        float step = 0f;
        while (!col1.Equals(col2))
        {
            yield return new WaitForSeconds(0.01f);
           this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(col1, col2, step += 0.005f));
        }
        yield return null;
    }
}

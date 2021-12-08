using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public float scroll_Speed = 0.0f;

    private MeshRenderer Mesh_Renderer;
    private float y_Scroll;
    void Start()
    {
        Mesh_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    void Scroll()
    {
        y_Scroll = Time.time * scroll_Speed;

        Vector2 offset = new Vector2(y_Scroll, 0f);
        Mesh_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}

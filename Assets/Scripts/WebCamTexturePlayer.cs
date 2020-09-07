using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    [RequireComponent(typeof(RawImage))]
    public class WebCamTexturePlayer : MonoBehaviour
    {
        [SerializeField] Shader _shader = null;
        Material _material;

        [SerializeField] Color _color = Color.white;

        Texture _webCamTexture;
        RawImage _image;

        void Start()
        {
            _material = new Material(_shader);
            _image = GetComponent<RawImage>();

            Debug.Log(WebCamTexture.devices.Length);
            for (int i = 0; i < WebCamTexture.devices.Length; i++)
            {
                Debug.Log(WebCamTexture.devices[i].name);
            }

            WebCamTexture webCamTexture = new WebCamTexture(WebCamTexture.devices[1].name);
            _webCamTexture = webCamTexture;

            webCamTexture.Play();
        }

        void Update()
        {
            RenderTexture dstTex = RenderTexture.GetTemporary(
                _webCamTexture.width,
                _webCamTexture.height,
                0, 
                RenderTextureFormat.ARGB32);

            _material.SetColor("_Color", _color);
            Graphics.Blit(_webCamTexture, dstTex, _material);
            _image.texture = dstTex;

            RenderTexture.ReleaseTemporary(dstTex);
        }
    }
}

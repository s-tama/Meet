//
// ImageEffect.cs
//
// Author : 
//

using UnityEngine;

public class ImageEffect : MonoBehaviour
{
    //[SerializeField] Camera _camera = null;
    [SerializeField] Material _material = null;
    [SerializeField] RenderTexture _srcTex = null;
    [SerializeField] RenderTexture _dstTex = null;

    private void OnPostRender()
    {
        //if(Camera.current != _camera) return;

        //_material.SetTexture("_MainTex", _srcTex);
        Graphics.Blit(_srcTex, _dstTex, _material);
    }

    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Graphics.Blit(source, destination, _material);
    //    Graphics.Blit(destination, _rTex, _material);
    //}
}

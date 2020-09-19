using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Process = System.Diagnostics.Process;

namespace MyProject
{
    [RequireComponent(typeof(RawImage))]
    public class WebCamTexturePlayer : MonoBehaviour
    {
        // プロセスを起動させるbatファイルが格納されたディレクトリのパス
        static readonly string ProcessScriptPath = "/StreamingAssets/ProcessScripts/";

        [SerializeField] Shader _shader = null;
        Material _material;

        [SerializeField] Color _color = Color.white;

        [SerializeField] string _deviceName = "Camera";

        Texture _webCamTexture;
        RawImage _image;

        void Start()
        {
            if(_deviceName == "Snap Camera" )
            {
                CallBat( "Snap Camera.bat" );
                CallBat( "obs64.bat" );
            }

            StartWebCamera();
        }

        void Update()
        {
            if( _webCamTexture == null )
            {
                Debug.LogAssertion( "WebCamTextureの取得に失敗しています" );
                return;
            }

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

        /// <summary>
        /// バッチファイルを呼び出す
        /// </summary>
        void CallBat( string path )
        {
            Process process = new Process();
            process.StartInfo.FileName = Application.dataPath + ProcessScriptPath + path;

            // 外部プロセス終了を検知
            process.EnableRaisingEvents = true;
            process.Exited += ( sender, e ) =>
            {
                process.Dispose();
                process = null;
            };

            process.Start();
        }

        /// <summary>
        /// Webカメラの開始
        /// </summary>
        void StartWebCamera()
        {
            _material = new Material( _shader );
            _image = GetComponent<RawImage>();

            // デバイス一覧の表示
            Debug.Log( "デバイス数 : " + WebCamTexture.devices.Length );
            for( int i = 0; i < WebCamTexture.devices.Length; i++ )
            {
                Debug.Log( i + " : " + WebCamTexture.devices[ i ].name );
            }

            // ウェブカメラテクスチャの生成
            for( int i = 0; i < WebCamTexture.devices.Length; i++ )
            {
                if( WebCamTexture.devices[ i ].name == _deviceName )
                {
                    WebCamTexture webCamTexture = new WebCamTexture( WebCamTexture.devices[ i ].name );
                    _webCamTexture = webCamTexture;
                    webCamTexture.Play();
                    break;
                }
            }

            Debug.Assert( _webCamTexture != null, "WebCamTextureの取得に失敗しました" );
        }
    }
}

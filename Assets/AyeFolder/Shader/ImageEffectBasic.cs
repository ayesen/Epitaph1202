using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class ImageEffectBasic : MonoBehaviour
{
    public Material effectMaterial;
    //unity method do when rendering image; rendertexture is the asset that is updating every frame, the image that camera sees every frame
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, effectMaterial); //effect the camera every frame, give the source render texture every frame
                                                                                //look for _mainTex
                                                                                //fragment shader --> render targets
    }
    
    


}

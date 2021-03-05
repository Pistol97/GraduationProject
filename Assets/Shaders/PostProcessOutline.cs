using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PostProcessOutlineRenderer), PostProcessEvent.BeforeStack, "Roystan/Post Process Outline")]
public sealed class PostProcessOutline : PostProcessEffectSettings
{
    public IntParameter scale = new IntParameter { value = 1 };

    public FloatParameter depthThreshold = new FloatParameter { value = 0.2f };

    [Range(0, 1)]
    public FloatParameter normalThreshold = new FloatParameter { value = 0.4f };

}

public sealed class PostProcessOutlineRenderer : PostProcessEffectRenderer<PostProcessOutline>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Roystan/Outline Post Process"));
        sheet.properties.SetFloat("_Scale", settings.scale);//Outline.shader의 변수를 가져온다.
        sheet.properties.SetFloat("_DepthThreshold", settings.depthThreshold);
        sheet.properties.SetFloat("_NormalThreshold", settings.normalThreshold);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
/* This is sourced from 'CubeCrafter'
 * URL: http://cubedefense.codeplex.com/SourceControl/changeset/view/16830
 * Need to create my own Version
*/

float4x4 ViewProjection;

float3 LightDirection = normalize(float3(-1, -1, -1));
float3 DiffuseLight = 1.25;
float3 AmbientLight = 0.25;

texture Texture;

sampler Sampler = sampler_state
{
    Texture = (Texture);
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal : NORMAL0;
    float2 TextureCoordinate : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TextureCoordinate : TEXCOORD0;
};

VertexShaderOutput StaticVerticeShader(VertexShaderInput input)
{
    VertexShaderOutput output;
	   
    //Since the vertices positions are calculated ahead of time, they are already in world space.
    output.Position = mul(input.Position, ViewProjection);

    float diffuseAmount = max(-dot(input.Normal, LightDirection), 0);
    float3 lightingResult = saturate(diffuseAmount * DiffuseLight + AmbientLight);
    output.Color = float4(lightingResult, 1);

    output.TextureCoordinate = input.TextureCoordinate;

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    return tex2D(Sampler, input.TextureCoordinate) * input.Color;
}

technique StaticVertexBufferRendering
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 StaticVerticeShader();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}

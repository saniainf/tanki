sampler  ColorSampler  : register(s0);
sampler  ColorSampler2  : register(s1);
 
float4 FilterPS(float2 TexCoords : TEXCOORD0) : COLOR0
{
          float3 luminance = float3(0.299, 0.587, 0.114);
          float4 color = tex2D(ColorSampler, TexCoords);
          float intensity = dot(color, luminance);
          if (intensity < 0.9)
                    return float4(0,0,0,1);
          else
                    return float4(1,1,1,1);
}
 
float4 BlurPS(float2 TexCoords : TEXCOORD0) : COLOR0
{
          float step = 2.0;
          float2 deltaX = float2( step / 640, 0);
          float2 deltaY = float2( 0, step / 480);
         
          float4 color = float4(0,0,0,1);
         
          int i = 0;
          for (i = -3; i <= 3; i++)
                    color += tex2D(ColorSampler, TexCoords + i*deltaX);
                   
          for (i = -3; i <= 3; i++)
                    color += tex2D(ColorSampler, TexCoords + i*deltaY);
                   
          color -= tex2D(ColorSampler, TexCoords);
         
          color /= 13;
         
          return color;
}
 
float4 GlowPS(float2 TexCoords : TEXCOORD0) : COLOR0
{
          float4 color = tex2D(ColorSampler, TexCoords);
          float4 color2 = tex2D(ColorSampler2, TexCoords);
          return color2 + color;
}
 
 
 
technique Filter
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 FilterPS();
    }
}
technique Blur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 BlurPS();
    }
}
technique Glow
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 GlowPS();
    }
}
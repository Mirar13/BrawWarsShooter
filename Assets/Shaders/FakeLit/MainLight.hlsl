#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

float3 CalculateDirection()
{
    #ifdef SHADERGRAPH_PREVIEW
    return float3(-0.5, 0.5, -0.5);
    #else
    Light light = GetMainLight();
    return light.direction;
    #endif
}

float3 CalculateColor()
{
    #ifdef SHADERGRAPH_PREVIEW
    return float3(1, 1, 1);
    #else
    Light light = GetMainLight();
    return light.color;
    #endif
}


void MainLight_float(out float3 Direction, out float3 Color)
{
    Direction = CalculateDirection();
    Color = CalculateColor();
}

#endif
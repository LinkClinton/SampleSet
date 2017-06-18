#pragma pack_matrix(row_major)

struct VsInput
{
    float3 position : POSITION;
    float2 tex : TEXCOORD;
};

struct PsInput
{
    float4 positionH : SV_Position;
    float2 tex : TEXCOORD;
};

cbuffer Camera : register(b0)
{
    matrix view;
    matrix proj;
};

cbuffer Transform : register(b1)
{
    matrix world;
}

Texture2D Tex : register(t0);


SamplerState Sampler : register(s0)
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = CLAMP;
    AddressV = CLAMP;
};

PsInput vs_main(VsInput input)
{
    PsInput result;

    result.positionH = mul(float4(input.position, 1.f), world);
    result.positionH = mul(result.positionH, view);
    result.positionH = mul(result.positionH, proj);

    result.tex = input.tex;

    return result;
}

float4 ps_main(PsInput input) : SV_Target
{
    return Tex.Sample(Sampler, input.tex);
}

#pragma pack_matrix(row_major)

struct VsInput
{
    float3 position : POSITION;
};

struct PsInput
{
    float4 positionH : SV_Position;
};

cbuffer MainCamera : register(b0)
{
    matrix view;
    matrix proj;
};

cbuffer CubeBuffer : register(b1)
{
    matrix world;
    float4 color;
};

PsInput vs_main(VsInput input)
{
    PsInput result;

    result.positionH = mul(float4(input.position, 1.f), world);
    result.positionH = mul(result.positionH, view);
    result.positionH = mul(result.positionH, proj);
    
    return result;
}

float4 ps_main(PsInput input) : SV_Target
{
    return color;
}
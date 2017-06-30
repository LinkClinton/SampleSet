#pragma pack_matrix(row_major)

struct VsInput
{
    float3 position : POSITION;
};

struct PsInput
{
    float4 positionH : SV_Position;
    float depth : Depth;
};

struct PsOutput
{
    float4 color : SV_Target;
    float depth : SV_Depth;
};

cbuffer MainCamera : register(b0)
{
    matrix view;
    matrix proj;
    float4 position;
};

cbuffer CubeBuffer : register(b1)
{
    matrix world;
    float4 color;
};

PsInput vs_main(VsInput input)
{
    PsInput result;

    float4 pos = mul(float4(input.position, 1.f), world);

    result.depth = distance(pos.xyz, position.xyz) / 2000;
    
    result.positionH = mul(float4(input.position, 1.f), world);
    result.positionH = mul(result.positionH, view);
    result.positionH = mul(result.positionH, proj);
    
    
    return result;
}

PsOutput ps_main(PsInput input)
{
    PsOutput result = (PsOutput) 0;
    result.color = color;
    result.depth = input.depth;

    return result;
}
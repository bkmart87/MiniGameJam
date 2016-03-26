// Shader created with Shader Forge v1.21 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.21;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:3138,x:32741,y:32690,varname:node_3138,prsc:2|emission-2641-OUT,alpha-4755-A;n:type:ShaderForge.SFN_Color,id:7241,x:31920,y:32778,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_HsvToRgb,id:2641,x:32460,y:32741,varname:node_2641,prsc:2|H-8464-OUT,S-4453-OUT,V-4453-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:4453,x:32229,y:32600,varname:node_4453,prsc:2|IN-7241-RGB;n:type:ShaderForge.SFN_Time,id:6761,x:31833,y:32974,varname:node_6761,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:4755,x:31890,y:32477,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_4755,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2370,x:31748,y:33164,ptovrint:False,ptlb:NoiseTex,ptin:_NoiseTex,varname:node_2370,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:8464,x:32181,y:32909,varname:node_8464,prsc:2|A-6761-T,B-2370-R;proporder:7241-4755-2370;pass:END;sub:END;*/

Shader "Shader Forge/RainbowText" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _NoiseTex ("NoiseTex", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _NoiseTex; uniform float4 _NoiseTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
                float4 node_6761 = _Time + _TimeEditor;
                float4 _NoiseTex_var = tex2D(_NoiseTex,TRANSFORM_TEX(i.uv0, _NoiseTex));
                float4 node_4453_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_4453_p = lerp(float4(float4(_Color.rgb,0.0).zy, node_4453_k.wz), float4(float4(_Color.rgb,0.0).yz, node_4453_k.xy), step(float4(_Color.rgb,0.0).z, float4(_Color.rgb,0.0).y));
                float4 node_4453_q = lerp(float4(node_4453_p.xyw, float4(_Color.rgb,0.0).x), float4(float4(_Color.rgb,0.0).x, node_4453_p.yzx), step(node_4453_p.x, float4(_Color.rgb,0.0).x));
                float node_4453_d = node_4453_q.x - min(node_4453_q.w, node_4453_q.y);
                float node_4453_e = 1.0e-10;
                float3 node_4453 = float3(abs(node_4453_q.z + (node_4453_q.w - node_4453_q.y) / (6.0 * node_4453_d + node_4453_e)), node_4453_d / (node_4453_q.x + node_4453_e), node_4453_q.x);;
                float3 emissive = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((node_6761.g+_NoiseTex_var.r)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_4453.r)*node_4453.r);
                float3 finalColor = emissive;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

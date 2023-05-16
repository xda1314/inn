Shader "_old_UI/MaskedStrip"
{
  Properties
  {
      _MainTex ("Sprite Texture",2D) = "white"{}
      _StripTex ("Strip Texture",2D) = "white"{}
      _Color ("Tint",Color) = (1,1,1,1)
  }
  SubShader
  {
      Tags
      { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 0
      Pass // ind: 1, name: DEFAULT
      {
        Name "DEFAULT"
        Tags
        { 
        "CanUseSpriteAtlas" = "true"
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        }
        LOD 0
        // m_ProgramMask = 6
        CGPROGRAM
#pragma target 3.0
#pragma vertex vertexShader
#pragma fragment fragmentShader
#pragma multi_compile UNITY_UI_ALPHACLIP

        // ======================
        // SerializedProgram: progVertex

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 0
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 1
        // m_ShaderHardwareTier: 0

#ifdef UNITY_UI_ALPHACLIP

        //  m_ShaderRequirements: 
        float4 vertexShader(float4 v:POSITION) : SV_POSITION
        {
            return UnityObjectToClipPos(v); // IS STUB!!!!
        }
#endif // UNITY_UI_ALPHACLIP
        //
        // ++++++++++++++++

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 2
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
        //
        // ++++++++++++++++

        // ++++++++++++++++
        // Sub Program: 10 for m_GpuProgramType: #5
        // m_BlobIndex: 3
        // m_ShaderHardwareTier: 0

#ifdef UNITY_UI_ALPHACLIP

        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
#endif // UNITY_UI_ALPHACLIP
        //
        // ++++++++++++++++

        //
        // ======================


        ENDCG
      } // end phase
  }
  FallBack ""
  /* Disabemble: 
   https://blogs.unity3d.com/ru/2015/08/27/plans-for-graphics-features-deprecation/


 Block#0 Platform: 5 raw_size: 4392
04000000EC080000000800002400000074080000EC1000003C0000009808000054000000324F040C05000000140000000300000001000000000000000100000012000000554E4954595F55495F414C504841434C495000001D0800002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C6573436F6C6F723B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D206C6F77702076656334205F436F6C6F723B0A76617279696E67206C6F7770207665633420786C765F434F4C4F523B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244313B0A76617279696E67206869676870207665633420786C765F544558434F4F5244323B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A2020746D707661725F31203D205F676C65735665727465783B0A20206869676870207665633420746D707661725F323B0A2020746D707661725F32203D205F676C6573436F6C6F723B0A20206C6F7770207665633420746D707661725F333B0A20206869676870207665633420746D707661725F343B0A2020746D707661725F342E77203D20312E303B0A2020746D707661725F342E78797A203D20746D707661725F312E78797A3B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E78797A203D207665633328312E302C20312E302C20312E30293B0A2020746D707661725F352E77203D20746D707661725F322E773B0A2020746D707661725F33203D2028746D707661725F35202A205F436F6C6F72293B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3429293B0A2020786C765F434F4C4F52203D20746D707661725F333B0A2020786C765F544558434F4F524430203D205F676C65734D756C7469546578436F6F7264302E78793B0A2020786C765F544558434F4F524431203D20746D707661725F322E78793B0A2020786C765F544558434F4F524432203D20746D707661725F313B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F5465787475726553616D706C654164643B0A756E69666F726D2068696768702076656334205F436C6970526563743B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D2073616D706C65723244205F53747269705465783B0A76617279696E67206C6F7770207665633420786C765F434F4C4F523B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244313B0A76617279696E67206869676870207665633420786C765F544558434F4F5244323B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A20206D656469756D70207665633420636F6C6F725F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D20282874657874757265324420285F4D61696E5465782C20786C765F544558434F4F52443029202B205F5465787475726553616D706C6541646429202A20786C765F434F4C4F52293B0A2020636F6C6F725F32203D20746D707661725F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F53747269705465782C20786C765F544558434F4F524431293B0A20206D656469756D70207665633420746D707661725F353B0A2020746D707661725F35203D2028746D707661725F34202A20666C6F61742828636F6C6F725F322E77203E3D20312E302929293B0A2020636F6C6F725F322E78797A203D202828746D707661725F352E78797A202A20746D707661725F352E7729202B2028636F6C6F725F322E78797A202A2028312E30202D20746D707661725F352E772929293B0A2020686967687020666C6F617420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A2020746D707661725F372E78203D20666C6F617428285F436C6970526563742E7A203E3D20786C765F544558434F4F5244322E7829293B0A2020746D707661725F372E79203D20666C6F617428285F436C6970526563742E77203E3D20786C765F544558434F4F5244322E7929293B0A20206869676870207665633220746D707661725F383B0A2020746D707661725F38203D20287665633228677265617465725468616E457175616C2028786C765F544558434F4F5244322E78792C205F436C6970526563742E78792929202A20746D707661725F37293B0A2020746D707661725F36203D2028746D707661725F382E78202A20746D707661725F382E79293B0A2020636F6C6F725F322E77203D2028636F6C6F725F322E77202A20746D707661725F36293B0A20206D656469756D7020666C6F617420785F393B0A2020785F39203D2028636F6C6F725F322E77202D20302E303031293B0A20206966202828785F39203C20302E302929207B0A20202020646973636172643B0A20207D3B0A2020746D707661725F31203D20636F6C6F725F323B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A0000000D000000000000000100000000000000000000000000000000000000324F040C05000000000000000000000000000000000000000100000012000000554E4954595F55495F414C504841434C495000000000000000000000000000000100000000000000000000000000000000000000324F040C050000001200000002000000000000000000000000000000C40700002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C6573436F6C6F723B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D206C6F77702076656334205F436F6C6F723B0A76617279696E67206C6F7770207665633420786C765F434F4C4F523B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244313B0A76617279696E67206869676870207665633420786C765F544558434F4F5244323B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A2020746D707661725F31203D205F676C65735665727465783B0A20206869676870207665633420746D707661725F323B0A2020746D707661725F32203D205F676C6573436F6C6F723B0A20206C6F7770207665633420746D707661725F333B0A20206869676870207665633420746D707661725F343B0A2020746D707661725F342E77203D20312E303B0A2020746D707661725F342E78797A203D20746D707661725F312E78797A3B0A20206869676870207665633420746D707661725F353B0A2020746D707661725F352E78797A203D207665633328312E302C20312E302C20312E30293B0A2020746D707661725F352E77203D20746D707661725F322E773B0A2020746D707661725F33203D2028746D707661725F35202A205F436F6C6F72293B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3429293B0A2020786C765F434F4C4F52203D20746D707661725F333B0A2020786C765F544558434F4F524430203D205F676C65734D756C7469546578436F6F7264302E78793B0A2020786C765F544558434F4F524431203D20746D707661725F322E78793B0A2020786C765F544558434F4F524432203D20746D707661725F313B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F5465787475726553616D706C654164643B0A756E69666F726D2068696768702076656334205F436C6970526563743B0A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A756E69666F726D2073616D706C65723244205F53747269705465783B0A76617279696E67206C6F7770207665633420786C765F434F4C4F523B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244313B0A76617279696E67206869676870207665633420786C765F544558434F4F5244323B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A20206D656469756D70207665633420636F6C6F725F323B0A20206C6F7770207665633420746D707661725F333B0A2020746D707661725F33203D20282874657874757265324420285F4D61696E5465782C20786C765F544558434F4F52443029202B205F5465787475726553616D706C6541646429202A20786C765F434F4C4F52293B0A2020636F6C6F725F32203D20746D707661725F333B0A20206C6F7770207665633420746D707661725F343B0A2020746D707661725F34203D2074657874757265324420285F53747269705465782C20786C765F544558434F4F524431293B0A20206D656469756D70207665633420746D707661725F353B0A2020746D707661725F35203D2028746D707661725F34202A20666C6F61742828636F6C6F725F322E77203E3D20312E302929293B0A2020636F6C6F725F322E78797A203D202828746D707661725F352E78797A202A20746D707661725F352E7729202B2028636F6C6F725F322E78797A202A2028312E30202D20746D707661725F352E772929293B0A2020686967687020666C6F617420746D707661725F363B0A20206869676870207665633220746D707661725F373B0A2020746D707661725F372E78203D20666C6F617428285F436C6970526563742E7A203E3D20786C765F544558434F4F5244322E7829293B0A2020746D707661725F372E79203D20666C6F617428285F436C6970526563742E77203E3D20786C765F544558434F4F5244322E7929293B0A20206869676870207665633220746D707661725F383B0A2020746D707661725F38203D20287665633228677265617465725468616E457175616C2028786C765F544558434F4F5244322E78792C205F436C6970526563742E78792929202A20746D707661725F37293B0A2020746D707661725F36203D2028746D707661725F382E78202A20746D707661725F382E79293B0A2020636F6C6F725F322E77203D2028636F6C6F725F322E77202A20746D707661725F36293B0A2020746D707661725F31203D20636F6C6F725F323B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A0D000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000

 Block#1 Platform: 9 raw_size: 6080
040000009C0C0000240B0000B4000000E80B0000240000003C0000006000000054000000324F040C0400000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C04000000000000000000000000000000000000000100000012000000554E4954595F55495F414C504841434C495000000000000000000000000000000100000000000000000000000000000000000000324F040C04000000000000000000000000000000000000000100000012000000554E4954595F55495F414C504841434C49500000910B0000236966646566205645525445580A2376657273696F6E203330302065730A0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B345D3B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4D617472697856505B345D3B0A756E69666F726D20096D656469756D702076656334205F436F6C6F723B0A696E206869676870207665633420696E5F504F534954494F4E303B0A696E206869676870207665633420696E5F434F4C4F52303B0A696E206869676870207665633220696E5F544558434F4F5244303B0A6F7574206D656469756D7020766563342076735F434F4C4F52303B0A6F757420686967687020766563322076735F544558434F4F5244303B0A6F757420686967687020766563322076735F544558434F4F5244313B0A6F757420686967687020766563342076735F544558434F4F5244323B0A7665633420755F786C6174303B0A7665633420755F786C6174313B0A766F6964206D61696E28290A7B0A20202020755F786C617430203D20696E5F504F534954494F4E302E79797979202A20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B315D3B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B305D202A20696E5F504F534954494F4E302E78787878202B20755F786C6174303B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B325D202A20696E5F504F534954494F4E302E7A7A7A7A202B20755F786C6174303B0A20202020755F786C617430203D20755F786C617430202B20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B335D3B0A20202020755F786C617431203D20755F786C6174302E79797979202A20686C736C63635F6D7478347834756E6974795F4D617472697856505B315D3B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B305D202A20755F786C6174302E78787878202B20755F786C6174313B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B325D202A20755F786C6174302E7A7A7A7A202B20755F786C6174313B0A20202020676C5F506F736974696F6E203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B335D202A20755F786C6174302E77777777202B20755F786C6174313B0A2020202076735F434F4C4F52302E78797A203D205F436F6C6F722E78797A3B0A2020202076735F434F4C4F52302E77203D20696E5F434F4C4F52302E77202A205F436F6C6F722E773B0A2020202076735F544558434F4F5244302E7879203D20696E5F544558434F4F5244302E78793B0A2020202076735F544558434F4F5244312E7879203D20696E5F434F4C4F52302E78793B0A2020202076735F544558434F4F524432203D20696E5F504F534954494F4E303B0A2020202072657475726E3B0A7D0A0A23656E6469660A23696664656620465241474D454E540A2376657273696F6E203330302065730A0A707265636973696F6E20686967687020696E743B0A756E69666F726D20096D656469756D702076656334205F5465787475726553616D706C654164643B0A756E69666F726D200976656334205F436C6970526563743B0A756E69666F726D206C6F77702073616D706C65723244205F4D61696E5465783B0A756E69666F726D206C6F77702073616D706C65723244205F53747269705465783B0A696E206D656469756D7020766563342076735F434F4C4F52303B0A696E20686967687020766563322076735F544558434F4F5244303B0A696E20686967687020766563322076735F544558434F4F5244313B0A696E20686967687020766563342076735F544558434F4F5244323B0A6C61796F7574286C6F636174696F6E203D203029206F7574206D656469756D7020766563342053565F546172676574303B0A7665633420755F786C6174303B0A6D656469756D70207665633420755F786C617431365F303B0A6C6F7770207665633420755F786C617431305F303B0A627665633420755F786C617462303B0A6D656469756D70207665633420755F786C617431365F313B0A6C6F7770207665633420755F786C617431305F313B0A6D656469756D70207665633320755F786C617431365F323B0A6D656469756D7020666C6F617420755F786C617431365F353B0A766F6964206D61696E28290A7B0A20202020755F786C617462302E7879203D20677265617465725468616E457175616C2876735F544558434F4F5244322E787978782C205F436C6970526563742E78797878292E78793B0A20202020755F786C617462302E7A77203D20677265617465725468616E457175616C285F436C6970526563742E7A7A7A772C2076735F544558434F4F5244322E78787879292E7A773B0A20202020755F786C617430203D206D6978287665633428302E302C20302E302C20302E302C20302E30292C207665633428312E302C20312E302C20312E302C20312E30292C207665633428755F786C6174623029293B0A20202020755F786C6174302E7879203D207665633228755F786C6174302E7A202A20755F786C6174302E782C20755F786C6174302E77202A20755F786C6174302E79293B0A20202020755F786C6174302E78203D20755F786C6174302E79202A20755F786C6174302E783B0A20202020755F786C617431305F31203D2074657874757265285F4D61696E5465782C2076735F544558434F4F5244302E7879293B0A20202020755F786C617431365F31203D20755F786C617431305F31202B205F5465787475726553616D706C654164643B0A20202020755F786C617431365F31203D20755F786C617431365F31202A2076735F434F4C4F52303B0A20202020755F786C617431365F322E78203D20755F786C617431365F312E77202A20755F786C6174302E78202B202D302E30303130303030303030353B0A20202020755F786C6174302E78203D20755F786C6174302E78202A20755F786C617431365F312E773B0A2020202053565F546172676574302E77203D20755F786C6174302E783B0A23696664656620554E4954595F414452454E4F5F4553330A20202020755F786C617462302E78203D20212128755F786C617431365F322E783C302E30293B0A23656C73650A20202020755F786C617462302E78203D20755F786C617431365F322E783C302E303B0A23656E6469660A2020202069662828696E7428755F786C617462302E7829202A20696E742830786666666666666666752929213D30297B646973636172643B7D0A23696664656620554E4954595F414452454E4F5F4553330A20202020755F786C617462302E78203D20212128755F786C617431365F312E773E3D312E30293B0A23656C73650A20202020755F786C617462302E78203D20755F786C617431365F312E773E3D312E303B0A23656E6469660A20202020755F786C617431365F322E78203D2028755F786C617462302E7829203F20312E30203A20302E303B0A20202020755F786C617431305F30203D2074657874757265285F53747269705465782C2076735F544558434F4F5244312E7879293B0A20202020755F786C617431365F35203D20282D755F786C617431305F302E7729202A20755F786C617431365F322E78202B20312E303B0A20202020755F786C617431365F30203D20755F786C617431365F322E78787878202A20755F786C617431305F303B0A20202020755F786C617431365F322E78797A203D20755F786C617431365F312E78797A202A207665633328755F786C617431365F35293B0A2020202053565F546172676574302E78797A203D20755F786C617431365F302E78797A202A20755F786C617431365F302E777777202B20755F786C617431365F322E78797A3B0A2020202072657475726E3B0A7D0A0A23656E6469660A0000000D000000000000000100000000000000000000000000000000000000324F040C040000000000000000000000000000000000000000000000E80A0000236966646566205645525445580A2376657273696F6E203330302065730A0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B345D3B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4D617472697856505B345D3B0A756E69666F726D20096D656469756D702076656334205F436F6C6F723B0A696E206869676870207665633420696E5F504F534954494F4E303B0A696E206869676870207665633420696E5F434F4C4F52303B0A696E206869676870207665633220696E5F544558434F4F5244303B0A6F7574206D656469756D7020766563342076735F434F4C4F52303B0A6F757420686967687020766563322076735F544558434F4F5244303B0A6F757420686967687020766563322076735F544558434F4F5244313B0A6F757420686967687020766563342076735F544558434F4F5244323B0A7665633420755F786C6174303B0A7665633420755F786C6174313B0A766F6964206D61696E28290A7B0A20202020755F786C617430203D20696E5F504F534954494F4E302E79797979202A20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B315D3B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B305D202A20696E5F504F534954494F4E302E78787878202B20755F786C6174303B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B325D202A20696E5F504F534954494F4E302E7A7A7A7A202B20755F786C6174303B0A20202020755F786C617430203D20755F786C617430202B20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B335D3B0A20202020755F786C617431203D20755F786C6174302E79797979202A20686C736C63635F6D7478347834756E6974795F4D617472697856505B315D3B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B305D202A20755F786C6174302E78787878202B20755F786C6174313B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B325D202A20755F786C6174302E7A7A7A7A202B20755F786C6174313B0A20202020676C5F506F736974696F6E203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B335D202A20755F786C6174302E77777777202B20755F786C6174313B0A2020202076735F434F4C4F52302E78797A203D205F436F6C6F722E78797A3B0A2020202076735F434F4C4F52302E77203D20696E5F434F4C4F52302E77202A205F436F6C6F722E773B0A2020202076735F544558434F4F5244302E7879203D20696E5F544558434F4F5244302E78793B0A2020202076735F544558434F4F5244312E7879203D20696E5F434F4C4F52302E78793B0A2020202076735F544558434F4F524432203D20696E5F504F534954494F4E303B0A2020202072657475726E3B0A7D0A0A23656E6469660A23696664656620465241474D454E540A2376657273696F6E203330302065730A0A707265636973696F6E20686967687020696E743B0A756E69666F726D20096D656469756D702076656334205F5465787475726553616D706C654164643B0A756E69666F726D200976656334205F436C6970526563743B0A756E69666F726D206C6F77702073616D706C65723244205F4D61696E5465783B0A756E69666F726D206C6F77702073616D706C65723244205F53747269705465783B0A696E206D656469756D7020766563342076735F434F4C4F52303B0A696E20686967687020766563322076735F544558434F4F5244303B0A696E20686967687020766563322076735F544558434F4F5244313B0A696E20686967687020766563342076735F544558434F4F5244323B0A6C61796F7574286C6F636174696F6E203D203029206F7574206D656469756D7020766563342053565F546172676574303B0A7665633220755F786C6174303B0A6D656469756D70207665633420755F786C617431365F303B0A6C6F7770207665633420755F786C617431305F303B0A627665633220755F786C617462303B0A7665633220755F786C6174313B0A6D656469756D70207665633420755F786C617431365F313B0A6C6F7770207665633420755F786C617431305F313B0A627665633220755F786C617462313B0A6D656469756D70207665633320755F786C617431365F323B0A6D656469756D7020666C6F617420755F786C617431365F353B0A766F6964206D61696E28290A7B0A20202020755F786C617431305F30203D2074657874757265285F4D61696E5465782C2076735F544558434F4F5244302E7879293B0A20202020755F786C617431365F30203D20755F786C617431305F30202B205F5465787475726553616D706C654164643B0A20202020755F786C617431365F30203D20755F786C617431365F30202A2076735F434F4C4F52303B0A23696664656620554E4954595F414452454E4F5F4553330A20202020755F786C617462312E78203D20212128755F786C617431365F302E773E3D312E30293B0A23656C73650A20202020755F786C617462312E78203D20755F786C617431365F302E773E3D312E303B0A23656E6469660A20202020755F786C617431365F322E78203D2028755F786C617462312E7829203F20312E30203A20302E303B0A20202020755F786C617431305F31203D2074657874757265285F53747269705465782C2076735F544558434F4F5244312E7879293B0A20202020755F786C617431365F35203D20282D755F786C617431305F312E7729202A20755F786C617431365F322E78202B20312E303B0A20202020755F786C617431365F31203D20755F786C617431365F322E78787878202A20755F786C617431305F313B0A20202020755F786C617431365F322E78797A203D20755F786C617431365F302E78797A202A207665633328755F786C617431365F35293B0A2020202053565F546172676574302E78797A203D20755F786C617431365F312E78797A202A20755F786C617431365F312E777777202B20755F786C617431365F322E78797A3B0A20202020755F786C617462302E7879203D20677265617465725468616E457175616C2876735F544558434F4F5244322E787978782C205F436C6970526563742E78797878292E78793B0A20202020755F786C6174302E7879203D206D6978287665633228302E302C20302E30292C207665633228312E302C20312E30292C207665633228755F786C617462302E787929293B0A20202020755F786C617462312E7879203D20677265617465725468616E457175616C285F436C6970526563742E7A777A7A2C2076735F544558434F4F5244322E78797878292E78793B0A20202020755F786C6174312E7879203D206D6978287665633228302E302C20302E30292C207665633228312E302C20312E30292C207665633228755F786C617462312E787929293B0A20202020755F786C6174302E7879203D20755F786C6174302E7879202A20755F786C6174312E78793B0A20202020755F786C6174302E78203D20755F786C6174302E79202A20755F786C6174302E783B0A20202020755F786C6174302E78203D20755F786C6174302E78202A20755F786C617431365F302E773B0A2020202053565F546172676574302E77203D20755F786C6174302E783B0A2020202072657475726E3B0A7D0A0A23656E6469660A0D000000000000000100000000000000000000000000000000000000

 Block#2 Platform: 18 raw_size: 5040
04000000F80900002809000024000000D4090000741300003C0000002013000054000000324F040C19000000000000000000000000000000000000000100000012000000554E4954595F55495F414C504841434C49500000D407000001000000D6040000FE0200002C000000AA0400000000000000000000000000000000000000000000000000004C4F4D530000010001000800AC0000000000000020110000910201D11002474C534C2E7374642E34353000000000A00400019F0F0404EDC2A5F306000D4953698D01A0020407100D1E0337030000470000230047000123100000021002220110002100003000000400100022001000210000010010021E0100030000010000030000010000010000010010001E00000100000100000300000300000A0000040010001E00000600000E00000B00000200000100100022001000210100010010011E02000300000200000100000200000100000100000100000100000100000100000100000100000100000100000100000100000100000100000100000100000100000100000100930202A1040202940206B7020206043E02060739080206A6020220B702020A043E02010B390C0201BE02060B0B3E02021039110202B502022001BB041302010000003E02020BB7020A06023E1A060B39270206BB040A0200000000EC040B0201010101BB040A020000803FEC040B0201010101B702080A02B502102000BB043802010000003E02060ABB0438060000000039270A068903020A010000000100AB0202433E020044394502003E0401303948020139270606BB04130400000000390C0A01B702060A033E02065639570206BB04380203000000BB040A0C6F1283BA3E12030B396802033E06030ABB041314FFFFFFFF3E16070A392712063945020039480401393A060639271006C60602A70200031802398101FA0107010BE701014D0B0201011013150E1214010B02014D0B02010110CE1607020A012D1A04020210010702134407020102040502032214001315021214010B02014D0B020101AB010B02144D0B02010101CE16070203012D1A020101B00107021C440702010200010405221D0001070E24D9140B020102047C0B02012E2A2C2E220700010B04092D30020101B0010B020B2D30020101104A30020301010B020E440B02010204050203220F00133A082839010A0201133A04283D010A02014A0A020401133A02283D22000101440C0101300601C70A0B020401220900010B040B131504124E010B02014B0B020401220500010B0206010B04014A0B020301220900133A0A4C59010A0201133A02283D010A02014A0A0203014B0A040201133A02583D220001133A02283D010A0201133A024C59010A02014A0A020301133A02283D220001133A06283D010A0201136C046959220002133A02583D010A0201C8160602014701070268D20A07020201002269000107026AC10A06020100D9141302016127C41013040201CB140602012AA71E0300BA1E00010318028C1E1802133A044C59010A0201CE160602015301070276D20A07020201002277000107067AC10A06020100A71E0300BA1E000105180222035A991E01180422055E991E021801010A0406133A02583D2200010144060101300401C70A0B020301220500133A048A0159010A02013F0A0201133A02583D010A02014A0A0203014B0A02016C220700015604414D0B02010100010B02114A0B020201220400010B02513D5602010118010A020FD00A56020101014A56020301224900010B020A3D5602010118010B020C3D56020101FC4A560203010156024F4B56020201010B0240440B020102040506032241008D1E88064C4F4D5300000100010008007400000000000000040B0000910201D11002474C534C2E7374642E34353000000000A0040001CF0F0004EDC2A5F306000B48525966686A6D100B1E001005061010010610470100230047000123403700020047000223800100000210022201100021014732000B004700010B014700020B03000002000C0010001E0000030000010010031E0100070010061E0110021E0210021E0210031E03930202A1040202A6020620B7020206043E020607390802063E020107390A0201B502062000BB040E0204000000BC0202070FBC0202070FCE02021011073E02021239130202B502022001BB04150200000000BB041502010000003E020207BB04151602000000BB0415120300000039080806BB040E2801000000BC02020644CE02020706453E020346394702033E10030739500403B702020603390A0C01BB040E02030000003E0201063E0602063E080306B7020406023E020364396502033E020164396702013965040339500603C60602D1010003180201070E014D0702010155231818141617010702014A07020E0122120023180214161601070201010702134D07020101004A07020301010702184B07020201221900231804141623010702010107021B4D07020101AA4A07020301010702204B070202012221000107022223180414162C010702014B07020401222600010704284D0702010155231802141717010702014A07020301220500231802141716010702010107022F4D07020101004A070203010107020B4B07020201220C0023180214172301070201010702364D07020101AA4A07020301010702124B0702020122130023180C14172C01070201010702424D07020101FF4A070203010107021E4B0702020113500448162200021318061423010702013D530201011801070205440702010204050603220600135B08595A01060201235E0414235A010602014A06020401136204525A22000201640C01220300010704122D640201011022020001070463220100236202481644010602013F0602012362024816442200018D1E88060D00000003000000000000000C000000020000000D000000030000000E000000030000000000000000000000000000001200000050476C6F62616C733431343730383538373400002000000002000000090000005F436C697052656374000000000000000100000004000000000000000000000010000000110000005F5465787475726553616D706C654164640000000000000001000000040000000000000000000000000000001200000056476C6F62616C733431343730383538373400009000000003000000060000005F436F6C6F7200000000000001000000040000000000000000000000800000000E000000756E6974795F4D61747269785650000000000000040000000400000001000000000000004000000013000000756E6974795F4F626A656374546F576F726C640000000000040000000400000001000000000000000000000004000000080000005F4D61696E546578000000000000000802000000090000005F53747269705465780000000000000001000008020100001200000050476C6F62616C733431343730383538373400000100000000000108000000001200000056476C6F62616C73343134373038353837340000010000000100010400000000324F040C1900000000000000000000000000000000000000000000003D070000000000003F040000FE0200002C000000130400000000000000000000000000000000000000000000000000004C4F4D5300000100010008009400000000000000F80E0000910201D11002474C534C2E7374642E34353000000000A00400019F0F0404EDC2A5F306001121425863A0020407000900000400100022001000210000010010031E010003000001003701000047000023004700012310000002100222011000210000050000010000010000020010001E00000100000100000900000700000900000300000100100022001000210100010010011E0200030000020000010000020000010000010000010000010000010000010000010000010000010000010000010000010000020010001E0000010000010000010000010000010000010000010010041E03002900930202A1040202A6020620B7020206043E0206073908020689030206010000000100AB02020A3E02000B390C0200B7020406023E02010F3910020139080606BE020407073E02021639170202B502022001BB041902000000003E0202073E0A010739200201940206B7020224023E02062539260206B502022000BB042802030000003E020606BB0406060000803FB7020806033E020631393202063E020706BB04060E00000000BB0428040000000039080406390C020039100401392A0606390810063E140307395702033926140639200201BB04190601000000B7020824043E06060F396D0206CC040F023434CC040F024343396D1606BB04280E010000003E1E0306C6060299020003180239346007010B4D01010F0801C70A07020501220A000107040C131B0E181A010702014B07020901220A000107020B010706014A07020401220F00132A10142901060201CE162404020101250208D20A250202010022090001250C0FC10A24020100A71E0300BA1E000105180222030B991E0118042205FFFFFFFF0F991E02180101060607132A04333D220002010B0601010F0401C70A07020301220500132A043F29010602013F060201132A02333D010602014A060203014B0602011F2207000131041B4D0702010100010702114A070202012204000107023E3D31020101180106020FD00A31020101014A310203012223000107060C3D31020101180107020E3D31020101FC4A310203010131022B4B3102020101070208440702010204050603220900010706014D0702010110131B041866010702014D0702010110CE166A0406022D2502010110220A0001250A0FD9140F020102037C0F02012E6F7072220500131B021866010702014D07020101BA010702144D0702010110CE166A0203012D250201011022530001250455D9140F02010D0E7C0F02012E6F707D220300010F0211010F02054A0F020201221300132A046E820101060201132A026E3D010602014A06020301132A026E3D220001132A026E3D01060201132A021429010602014A06020301132A026E3D220001132A026E3D010602011391010458292200028D1E88064C4F4D5300000100010008007400000000000000040B0000910201D11002474C534C2E7374642E34353000000000A0040001CF0F0004EDC2A5F306000B48525966686A6D100B1E001005061010010610470100230047000123403700020047000223800100000210022201100021014732000B004700010B014700020B03000002000C0010001E0000030000010010031E0100070010061E0110021E0210021E0210031E03930202A1040202A6020620B7020206043E020607390802063E020107390A0201B502062000BB040E0204000000BC0202070FBC0202070FCE02021011073E02021239130202B502022001BB04150200000000BB041502010000003E020207BB04151602000000BB0415120300000039080806BB040E2801000000BC02020644CE02020706453E020346394702033E10030739500403B702020603390A0C01BB040E02030000003E0201063E0602063E080306B7020406023E020364396502033E020164396702013965040339500603C60602D1010003180201070E014D0702010155231818141617010702014A07020E0122120023180214161601070201010702134D07020101004A07020301010702184B07020201221900231804141623010702010107021B4D07020101AA4A07020301010702204B070202012221000107022223180414162C010702014B07020401222600010704284D0702010155231802141717010702014A07020301220500231802141716010702010107022F4D07020101004A070203010107020B4B07020201220C0023180214172301070201010702364D07020101AA4A07020301010702124B0702020122130023180C14172C01070201010702424D07020101FF4A070203010107021E4B0702020113500448162200021318061423010702013D530201011801070205440702010204050603220600135B08595A01060201235E0414235A010602014A06020401136204525A22000201640C01220300010704122D640201011022020001070463220100236202481644010602013F0602012362024816442200018D1E88060000000D00000003000000000000000C000000020000000D000000030000000E000000030000000000000000000000000000001200000050476C6F62616C733336303834383632323500002000000002000000090000005F436C697052656374000000000000000100000004000000000000000000000010000000110000005F5465787475726553616D706C654164640000000000000001000000040000000000000000000000000000001200000056476C6F62616C733336303834383632323500009000000003000000060000005F436F6C6F7200000000000001000000040000000000000000000000800000000E000000756E6974795F4D61747269785650000000000000040000000400000001000000000000004000000013000000756E6974795F4F626A656374546F576F726C640000000000040000000400000001000000000000000000000004000000080000005F4D61696E546578000000000000000802000000090000005F53747269705465780000000000000001000008020100001200000050476C6F62616C733336303834383632323500000100000000000108000000001200000056476C6F62616C73333630383438363232350000010000000100010400000000324F040C19000000000000000000000000000000000000000100000012000000554E4954595F55495F414C504841434C495000000000000000000000000000000100000000000000000000000000000000000000324F040C1900000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000
  */
}

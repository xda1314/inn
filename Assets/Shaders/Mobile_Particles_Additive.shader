Shader "_old_Mobile/Particles/Additive"
{
  Properties
  {
      _MainTex ("Particle Texture",2D) = "white"{}
  }
  SubShader
  {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "PreviewType" = "Plane"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 0
      Pass // ind: 1, name: 
      {
        Tags
        { 
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

        //
        // ======================


        // ======================
        // SerializedProgram: progFragment

        // ++++++++++++++++
        // Sub Program: 1 for m_GpuProgramType: #5
        // m_BlobIndex: 1
        // m_ShaderHardwareTier: 0


        //  m_ShaderRequirements: 
        fixed4 fragmentShader() : SV_Target
        {
            return fixed4(1,1,1,1); // IS STUB!!!!
        }
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


 Block#0 Platform: 5 raw_size: 1072
0200000014000000E0030000F40300003C000000324F040C050000000100000001000000000000000000000000000000A20300002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C6573436F6C6F723B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A76617279696E67206C6F7770207665633420786C765F434F4C4F52303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A20206D656469756D70207665633420746D707661725F323B0A2020746D707661725F32203D20636C616D7020285F676C6573436F6C6F722C20302E302C20312E30293B0A2020746D707661725F31203D20746D707661725F323B0A20206869676870207665633420746D707661725F333B0A2020746D707661725F332E77203D20312E303B0A2020746D707661725F332E78797A203D205F676C65735665727465782E78797A3B0A2020786C765F434F4C4F5230203D20746D707661725F313B0A2020786C765F544558434F4F524430203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3329293B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A76617279696E67206C6F7770207665633420786C765F434F4C4F52303B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D202874657874757265324420285F4D61696E5465782C20786C765F544558434F4F52443029202A20786C765F434F4C4F5230293B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A00000D000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000

 Block#1 Platform: 9 raw_size: 1644
02000000500000001C060000140000003C000000324F040C0400000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C040000000000000000000000000000000000000000000000DF050000236966646566205645525445580A2376657273696F6E203330302065730A0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B345D3B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4D617472697856505B345D3B0A756E69666F726D200976656334205F4D61696E5465785F53543B0A696E206869676870207665633320696E5F504F534954494F4E303B0A696E206D656469756D70207665633420696E5F434F4C4F52303B0A696E206869676870207665633320696E5F544558434F4F5244303B0A6F7574206D656469756D7020766563342076735F434F4C4F52303B0A6F757420686967687020766563322076735F544558434F4F5244303B0A7665633420755F786C6174303B0A7665633420755F786C6174313B0A766F6964206D61696E28290A7B0A2020202076735F434F4C4F5230203D20696E5F434F4C4F52303B0A23696664656620554E4954595F414452454E4F5F4553330A2020202076735F434F4C4F5230203D206D696E286D61782876735F434F4C4F52302C20302E30292C20312E30293B0A23656C73650A2020202076735F434F4C4F5230203D20636C616D702876735F434F4C4F52302C20302E302C20312E30293B0A23656E6469660A2020202076735F544558434F4F5244302E7879203D20696E5F544558434F4F5244302E7879202A205F4D61696E5465785F53542E7879202B205F4D61696E5465785F53542E7A773B0A20202020755F786C617430203D20696E5F504F534954494F4E302E79797979202A20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B315D3B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B305D202A20696E5F504F534954494F4E302E78787878202B20755F786C6174303B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B325D202A20696E5F504F534954494F4E302E7A7A7A7A202B20755F786C6174303B0A20202020755F786C617430203D20755F786C617430202B20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B335D3B0A20202020755F786C617431203D20755F786C6174302E79797979202A20686C736C63635F6D7478347834756E6974795F4D617472697856505B315D3B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B305D202A20755F786C6174302E78787878202B20755F786C6174313B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B325D202A20755F786C6174302E7A7A7A7A202B20755F786C6174313B0A20202020676C5F506F736974696F6E203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B335D202A20755F786C6174302E77777777202B20755F786C6174313B0A2020202072657475726E3B0A7D0A0A23656E6469660A23696664656620465241474D454E540A2376657273696F6E203330302065730A0A707265636973696F6E20686967687020696E743B0A756E69666F726D206C6F77702073616D706C65723244205F4D61696E5465783B0A696E206D656469756D7020766563342076735F434F4C4F52303B0A696E20686967687020766563322076735F544558434F4F5244303B0A6C61796F7574286C6F636174696F6E203D203029206F7574206D656469756D7020766563342053565F546172676574303B0A6C6F7770207665633420755F786C617431305F303B0A766F6964206D61696E28290A7B0A20202020755F786C617431305F30203D2074657874757265285F4D61696E5465782C2076735F544558434F4F5244302E7879293B0A2020202053565F54617267657430203D20755F786C617431305F30202A2076735F434F4C4F52303B0A2020202072657475726E3B0A7D0A0A23656E6469660A000D000000000000000100000000000000000000000000000000000000

 Block#2 Platform: 18 raw_size: 1420
02000000140000003C050000500500003C000000324F040C190000000000000000000000000000000000000000000000050400000000000013010000F20200002C000000E70000000000000000000000000000000000000000000000000000004C4F4D5300000100010008001C00000000000000D0020000910201D11002474C534C2E7374642E34353000000000A0040001FF0E0404EDC2A5F30600111518A0020407000900000400100022001000210000010010031E0100040010001E0000010000020010001E00000100000100930202A1040202A6020620B7020206043E0206073908020689030206010000000100AB02020A3E02000B390C0200B7020406023E02010F391002013E060307391402033E04010739170201C606022700031802010B1201010F0801C70A07020501220A000107060D010706014A070204012205008D1E88064C4F4D5300000100010008007100000000000000BC0A0000910201D11002474C534C2E7374642E34353000000000A0040001AF0F0004EDC2A5F30600090B15182F6200090010001E0000020010001E0100010000010000030000010000010010031E0110031E021005061010010610470100230047000123404700022380010000021002220110002100100E1E004731000B004700010B014700020B03000002930202A1040202A6020620B7020206043E020307390802033E020107390A0201BB04060600000000BB0406020000803FB7020806023E02031339140203B7020206033E02011639170201B502062000BB041B0204000000BC0202071CBC0202071CCE02021D1E073E02021F39200202B502022001BB042202020000003E0202073E120607392D020639170201BB04220600000000BB04220201000000BB04222603000000392D0806BB041B2801000000BC0202065ECE020207065F3E020360396102033E120306C60602CD010003180201070E0122030001070204E00A070602020202E00A0702020202027C0702012B0D101122090001160E012D13020101101324162123010702012D13020101104A13020E011324022123010702012D13020101B04B13020401221700011608014D0702010155232406213233010702014A07020501220800232402213232010702010116020A4D07020101004A070203010107020E4B07020201220F0023240221322301070201011602114D07020101AA4A07020301010702154B0702020122160001070217232404213246010702014B07020401221B000107041D4D0702010155232402213333010702014A0702030122050023240221333201070201010702244D07020101004A070203010107020B4B07020201220C00232402213323010702010107022B4D07020101AA4A07020301010702124B0702020122130023240C21334601070201010702374D07020101FF4A070203010107021E4B070202011308026232220001236B0462325E010602013F060201236B0262325E2200018D1E88060000000D00000003000000020000000D000000030000000E000000000000000C000000020000000000000000000000000000001200000056476C6F62616C7333323735303833373733000090000000030000000B0000005F4D61696E5465785F5354000000000001000000040000000000000000000000800000000E000000756E6974795F4D61747269785650000000000000040000000400000001000000000000004000000013000000756E6974795F4F626A656374546F576F726C640000000000040000000400000001000000000000000000000002000000080000005F4D61696E5465780000000000000008020000001200000056476C6F62616C73333237353038333737330000010000000000010400000000324F040C1900000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000
  */
}

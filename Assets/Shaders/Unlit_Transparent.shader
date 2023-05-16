Shader "_old_Unlit/Transparent"
{
  Properties
  {
      _MainTex ("Base (RGB) Trans (A)",2D) = "white"{}
  }
  SubShader
  {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 100
      Pass // ind: 1, name: 
      {
        Tags
        { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
        }
        LOD 100
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


 Block#0 Platform: 5 raw_size: 832
0200000014000000F0020000040300003C000000324F040C050000000000000001000000000000000000000000000000B20200002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D2068696768702076656334205F4D61696E5465785F53543B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206869676870207665633420746D707661725F313B0A2020746D707661725F312E77203D20312E303B0A2020746D707661725F312E78797A203D205F676C65735665727465782E78797A3B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3129293B0A2020786C765F544558434F4F524430203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F4D61696E5465785F53542E787929202B205F4D61696E5465785F53542E7A77293B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D2073616D706C65723244205F4D61696E5465783B0A76617279696E67206869676870207665633220786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A2020746D707661725F31203D2074657874757265324420285F4D61696E5465782C20786C765F544558434F4F524430293B0A2020676C5F46726167446174615B305D203D20746D707661725F313B0A7D0A0A0A23656E6469660A000009000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000

 Block#1 Platform: 9 raw_size: 1396
020000005000000024050000140000003C000000324F040C0400000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C040000000000000000000000000000000000000000000000E6040000236966646566205645525445580A2376657273696F6E203330302065730A0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B345D3B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4D617472697856505B345D3B0A756E69666F726D200976656334205F4D61696E5465785F53543B0A696E206869676870207665633420696E5F504F534954494F4E303B0A696E206869676870207665633220696E5F544558434F4F5244303B0A6F757420686967687020766563322076735F544558434F4F5244303B0A7665633420755F786C6174303B0A7665633420755F786C6174313B0A766F6964206D61696E28290A7B0A20202020755F786C617430203D20696E5F504F534954494F4E302E79797979202A20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B315D3B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B305D202A20696E5F504F534954494F4E302E78787878202B20755F786C6174303B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B325D202A20696E5F504F534954494F4E302E7A7A7A7A202B20755F786C6174303B0A20202020755F786C617430203D20755F786C617430202B20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B335D3B0A20202020755F786C617431203D20755F786C6174302E79797979202A20686C736C63635F6D7478347834756E6974795F4D617472697856505B315D3B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B305D202A20755F786C6174302E78787878202B20755F786C6174313B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B325D202A20755F786C6174302E7A7A7A7A202B20755F786C6174313B0A20202020676C5F506F736974696F6E203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B335D202A20755F786C6174302E77777777202B20755F786C6174313B0A2020202076735F544558434F4F5244302E7879203D20696E5F544558434F4F5244302E7879202A205F4D61696E5465785F53542E7879202B205F4D61696E5465785F53542E7A773B0A2020202072657475726E3B0A7D0A0A23656E6469660A23696664656620465241474D454E540A2376657273696F6E203330302065730A0A707265636973696F6E20686967687020696E743B0A756E69666F726D206C6F77702073616D706C65723244205F4D61696E5465783B0A696E20686967687020766563322076735F544558434F4F5244303B0A6C61796F7574286C6F636174696F6E203D203029206F7574206D656469756D7020766563342053565F546172676574303B0A6C6F7770207665633420755F786C617431305F303B0A766F6964206D61696E28290A7B0A20202020755F786C617431305F30203D2074657874757265285F4D61696E5465782C2076735F544558434F4F5244302E7879293B0A2020202053565F54617267657430203D20755F786C617431305F303B0A2020202072657475726E3B0A7D0A0A23656E6469660A000009000000000000000100000000000000000000000000000000000000

 Block#2 Platform: 18 raw_size: 1276
0200000050000000AC040000140000003C000000324F040C1900000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C1900000000000000000000000000000000000000000000007E03000000000000F40000008A0200002C000000C80000000000000000000000000000000000000000000000000000004C4F4D530000010001000800180000000000000054020000910201D11002474C534C2E7374642E34353000000000A0040001EF0E0404EDC2A5F306001115A0020407000900000400100022001000210000010010031E0000040010001E00000100930202A1040202A6020620B7020206043E0206073908020689030206010000000100AB02020A3E02000B390C0200B7020406023E02010F391002013E06030739140203C606022100031802010B1201010F0801C70A07020501220A000107060D2201008D1E88064C4F4D530000010001000800660000000000000044090000910201D11002474C534C2E7374642E34353000000000A00400018F0F0004EDC2A5F306000B485456100B1E0010050610100106104701002300470001234047000223800100000210022201100021004732000B004700010B014700020B03000002100E1E0010021E01930202A1040202A6020620B7020206043E020607390802063E020107390A0201B502062000BB040E0204000000BC0202070FBC0202070FCE02021011073E02021239130202B502022001BB04150200000000BB041502010000003E020207BB04151602000000BB0415120300000039080806BB040E2801000000BC02020644CE02020706453E020346394702033E100307B7020406023E020352395302033E020152395502013E140306C60602B7010003180201070E014D0702010155231818141617010702014A07020E0122120023180214161601070201010702134D07020101004A07020301010702184B07020201221900231804141623010702010107021B4D07020101AA4A07020301010702204B070202012221000107022223180414162C010702014B07020401222600010704284D0702010155231802141717010702014A07020301220500231802141716010702010107022F4D07020101004A070203010107020B4B07020201220C0023180214172301070201010702364D07020101AA4A07020301010702124B0702020122130023180C14172C01070201010702424D07020101FF4A070203010107021E4B07020201135004481622000201520C011318021423010702012D52020101104A520204011318021423010702012D52020101B04B52020401220B00236004481644010602013F0602012360024816442200018D1E880600000900000002000000000000000C000000030000000D000000020000000000000000000000000000001200000056476C6F62616C7332323733363033313738000090000000030000000B0000005F4D61696E5465785F5354000000000001000000040000000000000000000000800000000E000000756E6974795F4D61747269785650000000000000040000000400000001000000000000004000000013000000756E6974795F4F626A656374546F576F726C640000000000040000000400000001000000000000000000000002000000080000005F4D61696E5465780000000000000008020000001200000056476C6F62616C73323237333630333137380000010000000000010400000000
  */
}

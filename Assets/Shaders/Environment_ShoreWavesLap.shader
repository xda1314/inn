Shader "_old_Environment/ShoreWavesLap"
{
  Properties
  {
      _Color ("Color",Color) = (1,1,1,1)
      _Wave ("Wave",2D) = "white"{}
      _Noise ("Noise",2D) = "white"{}
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
      Pass // ind: 1, name: WAVELAP
      {
        Name "WAVELAP"
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


 Block#0 Platform: 5 raw_size: 1552
0200000014000000C0050000D40500003C000000324F040C050000000300000002000000000000000000000000000000810500002376657273696F6E203130300A0A236966646566205645525445580A6174747269627574652076656334205F676C65735665727465783B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264303B0A6174747269627574652076656334205F676C65734D756C7469546578436F6F7264313B0A756E69666F726D2068696768702076656334205F54696D653B0A756E69666F726D206869676870206D61743420756E6974795F4F626A656374546F576F726C643B0A756E69666F726D206869676870206D61743420756E6974795F4D617472697856503B0A756E69666F726D206C6F77702076656334205F576176655F53543B0A756E69666F726D206C6F77702076656334205F4E6F6973655F53543B0A756E69666F726D20686967687020666C6F6174205F50756C736553706565643B0A76617279696E67206C6F7770207665633420786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206C6F7770207665633420746D707661725F313B0A20206869676870207665633420746D707661725F323B0A2020746D707661725F322E77203D20312E303B0A2020746D707661725F322E78797A203D205F676C65735665727465782E78797A3B0A2020746D707661725F312E7879203D2028285F676C65734D756C7469546578436F6F7264302E7879202A205F576176655F53542E787929202B205F576176655F53542E7A77293B0A2020686967687020666C6F617420746D707661725F333B0A2020746D707661725F33203D20667261637428285F54696D652E78202A205F50756C7365537065656429293B0A2020746D707661725F312E79203D2028746D707661725F312E79202D20746D707661725F33293B0A2020746D707661725F312E7A77203D2028285F676C65734D756C7469546578436F6F7264312E7879202A205F4E6F6973655F53542E787929202B205F4E6F6973655F53542E7A77293B0A2020676C5F506F736974696F6E203D2028756E6974795F4D61747269785650202A2028756E6974795F4F626A656374546F576F726C64202A20746D707661725F3229293B0A2020786C765F544558434F4F524430203D20746D707661725F313B0A7D0A0A0A23656E6469660A23696664656620465241474D454E540A756E69666F726D206C6F77702076656334205F436F6C6F723B0A756E69666F726D2073616D706C65723244205F576176653B0A756E69666F726D2073616D706C65723244205F4E6F6973653B0A756E69666F726D20686967687020666C6F6174205F4272696768746E6573733B0A76617279696E67206C6F7770207665633420786C765F544558434F4F5244303B0A766F6964206D61696E2028290A7B0A20206C6F777020666C6F617420616C7068615F313B0A2020686967687020666C6F617420746D705F323B0A20206C6F777020666C6F617420746D707661725F333B0A2020746D707661725F33203D20636C616D70202874657874757265324420285F4E6F6973652C20786C765F544558434F4F5244302E7A77292E782C20302E302C2074657874757265324420285F576176652C20786C765F544558434F4F5244302E7879292E78293B0A2020746D705F32203D20746D707661725F333B0A2020686967687020666C6F617420746D707661725F343B0A2020746D707661725F34203D20636C616D70202828746D705F32202A205F4272696768746E657373292C20302E302C20312E30293B0A2020616C7068615F31203D20746D707661725F343B0A20206C6F7770207665633420746D707661725F353B0A2020746D707661725F352E78797A203D205F436F6C6F722E78797A3B0A2020746D707661725F352E77203D20616C7068615F313B0A2020676C5F46726167446174615B305D203D20746D707661725F353B0A7D0A0A0A23656E6469660A00000019000000000000000100000000000000000000000000000000000000324F040C0500000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000

 Block#1 Platform: 9 raw_size: 2268
02000000500000008C080000140000003C000000324F040C0400000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000324F040C04000000000000000000000000000000000000000000000050080000236966646566205645525445580A2376657273696F6E203330302065730A0A756E69666F726D200976656334205F54696D653B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B345D3B0A756E69666F726D20097665633420686C736C63635F6D7478347834756E6974795F4D617472697856505B345D3B0A756E69666F726D20096D656469756D702076656334205F576176655F53543B0A756E69666F726D20096D656469756D702076656334205F4E6F6973655F53543B0A756E69666F726D2009666C6F6174205F50756C736553706565643B0A696E206869676870207665633420696E5F504F534954494F4E303B0A696E206D656469756D70207665633420696E5F544558434F4F5244303B0A696E206D656469756D70207665633420696E5F544558434F4F5244313B0A6F7574206D656469756D7020766563342076735F544558434F4F5244303B0A7665633420755F786C6174303B0A7665633420755F786C6174313B0A6D656469756D70207665633220755F786C617431365F323B0A766F6964206D61696E28290A7B0A20202020755F786C617430203D20696E5F504F534954494F4E302E79797979202A20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B315D3B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B305D202A20696E5F504F534954494F4E302E78787878202B20755F786C6174303B0A20202020755F786C617430203D20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B325D202A20696E5F504F534954494F4E302E7A7A7A7A202B20755F786C6174303B0A20202020755F786C617430203D20755F786C617430202B20686C736C63635F6D7478347834756E6974795F4F626A656374546F576F726C645B335D3B0A20202020755F786C617431203D20755F786C6174302E79797979202A20686C736C63635F6D7478347834756E6974795F4D617472697856505B315D3B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B305D202A20755F786C6174302E78787878202B20755F786C6174313B0A20202020755F786C617431203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B325D202A20755F786C6174302E7A7A7A7A202B20755F786C6174313B0A20202020676C5F506F736974696F6E203D20686C736C63635F6D7478347834756E6974795F4D617472697856505B335D202A20755F786C6174302E77777777202B20755F786C6174313B0A20202020755F786C6174302E78203D205F54696D652E78202A205F50756C736553706565643B0A20202020755F786C6174302E78203D20667261637428755F786C6174302E78293B0A20202020755F786C617431365F322E7879203D20696E5F544558434F4F5244302E7879202A205F576176655F53542E7879202B205F576176655F53542E7A773B0A20202020755F786C6174302E78203D20282D755F786C6174302E7829202B20755F786C617431365F322E793B0A2020202076735F544558434F4F5244302E78203D20755F786C617431365F322E783B0A2020202076735F544558434F4F5244302E79203D20755F786C6174302E783B0A2020202076735F544558434F4F5244302E7A77203D20696E5F544558434F4F5244312E7879202A205F4E6F6973655F53542E7879202B205F4E6F6973655F53542E7A773B0A2020202072657475726E3B0A7D0A0A23656E6469660A23696664656620465241474D454E540A2376657273696F6E203330302065730A0A707265636973696F6E20686967687020696E743B0A756E69666F726D20096D656469756D702076656334205F436F6C6F723B0A756E69666F726D2009666C6F6174205F4272696768746E6573733B0A756E69666F726D206C6F77702073616D706C65723244205F4E6F6973653B0A756E69666F726D206C6F77702073616D706C65723244205F576176653B0A696E206D656469756D7020766563342076735F544558434F4F5244303B0A6C61796F7574286C6F636174696F6E203D203029206F7574206D656469756D7020766563342053565F546172676574303B0A666C6F617420755F786C6174303B0A6D656469756D7020666C6F617420755F786C617431365F303B0A6C6F777020666C6F617420755F786C617431305F303B0A6C6F777020666C6F617420755F786C617431305F313B0A766F6964206D61696E28290A7B0A20202020755F786C617431305F30203D2074657874757265285F4E6F6973652C2076735F544558434F4F5244302E7A77292E783B0A20202020755F786C617431365F30203D206D617828755F786C617431305F302C20302E30293B0A20202020755F786C617431305F31203D2074657874757265285F576176652C2076735F544558434F4F5244302E7879292E783B0A20202020755F786C617431365F30203D206D696E28755F786C617431305F312C20755F786C617431365F30293B0A20202020755F786C617430203D20755F786C617431365F30202A205F4272696768746E6573733B0A23696664656620554E4954595F414452454E4F5F4553330A20202020755F786C617430203D206D696E286D617828755F786C6174302C20302E30292C20312E30293B0A23656C73650A20202020755F786C617430203D20636C616D7028755F786C6174302C20302E302C20312E30293B0A23656E6469660A2020202053565F546172676574302E77203D20755F786C6174303B0A2020202053565F546172676574302E78797A203D205F436F6C6F722E78797A3B0A2020202072657475726E3B0A7D0A0A23656E6469660A19000000000000000100000000000000000000000000000000000000

 Block#2 Platform: 18 raw_size: 2232
0200000014000000680800007C0800003C000000324F040C1900000000000000000000000000000000000000000000001B0600000000000044020000D70300002C000000180200000000000000000000000000000000000000000000000000004C4F4D530000010001000800430000000000000028070000910201D11002474C534C2E7374642E34353000000000A0040001EF0E0404EDC2A5F306001035A0020407000800000400100022001000210000010000030010001E00000200000100000400000100000100000200000100000100100022001000210100010000010000010000020000010000010000010000020037010000470000230047000123100000021002220110002100000B0010001E00000900000100930202A1040202A60206203E0206063907020689030206010000000100AB0202093E02000A390B0200B7020406043E02010E390F0201B702020602B502082000BB0415020000000039070406BB0406040000000039070406390B020039071206BE02040E063E02022839290202B502022001BB042B02010000003E020206BB04060A0000803F3E04030E39340203BB041504030000003E020306BB042B0400000000B7020206033E02020EC606026F00031802010A1001010E0A022D11020101B0C70A0E020701C10A06060300220F00010604116C06040128191A220300010A0601010E020F2D1102010110C70A0E020301C10A06020100220600010602070106020C6C060201252324220D000106040F132D0E2A2C010602014A06020901220A000106020B7C0604012B311A32220D00010606101338063537220003133C082A3A010E02013D3B02010118010E020B440E02010204050603220C008D1E88064C4F4D5300000100010008009000000000000000480E0000910201D11002474C534C2E7374642E34353000000000A00400019F0F0004EDC2A5F306000B4863757D100B1E001005061010010610470100230047000123104700022350370003004700032390013700040047000423A00147000523B00100000210022201100021014732000B004700010B014700020B03000002001C0000010010001E0100010000010000020000010000010000020000010000010000050000030010001E0000020000060010001E02000100000100000300000100000100000200000100000100930202A1040202A6020620B7020206043E020607390802063E020107390A0201B502062000BB040E0204000000BC0202070FBC0202070FFE02020710110707063E02021239130202B502022001BB041502010000003E020207BB04150800000000BB04151002000000BB0415120300000039080806BB040E2801000000BC02020644CE02020706453E020346394702033E100307BB040E04000000003E020206BB041506050000003E080606B7020C06023E02066039610206390A0201395024033E060306390A0A01BB04150604000000C60602F7010003180201070E014D0702010155231716141616010702014A07020D0122110023170414161B01070201010702134D07020101004A07020301010702184B07020201221900231704141623010702010107021B4D07020101AA4A07020301010702204B070202012221000107022223170414162C010702014B07020401222600010704284D0702010155231702142316010702014A0702030122050023170214231B010702010107022F4D07020101004A070203010107020B4B07020201220C0023170214232301070201010702364D07020101AA4A07020301010702124B0702020122130023170C14232C01070201010702424D07020101FF4A070203010107021E4B07020201135004481B220002235306141B52010602011353041456010602014A06020401135A040952220002135A020952010602015C0602010A5D135A02095222000101070A012D6002010110131702142C010702012D60020101104A60020401131702142C010702012D60020101B04B60020401220B00135A020952010602013F060201135A026244010602014B06020301135A020952220001135A046252010602011378047552220002135A020952010602011378027544220001010704012D6002010110131704148001010702012D60020101104A60020501131702148001010702012D60020101B04B6002040101070214440702010200010405221500237802481B44010602013F060201237802481B442200018D1E8806001900000003000000000000000C000000030000000D000000040000000E000000030000000000000000000000000000001100000050476C6F62616C7333323433303532393100000014000000020000000B0000005F4272696768746E65737300000000000100000001000000000000000000000010000000060000005F436F6C6F7200000000000001000000040000000000000000000000000000001100000056476C6F62616C73333234333035323931000000B400000006000000090000005F4E6F6973655F53540000000000000001000000040000000000000000000000A00000000B0000005F50756C73655370656564000000000001000000010000000000000000000000B0000000050000005F54696D65000000000000000100000004000000000000000000000000000000080000005F576176655F53540000000001000000040000000000000000000000900000000E000000756E6974795F4D61747269785650000000000000040000000400000001000000000000005000000013000000756E6974795F4F626A656374546F576F726C640000000000040000000400000001000000000000001000000004000000060000005F4E6F6973650000000000000000000802000000050000005F576176650000000000000001000008020100001100000050476C6F62616C733332343330353239310000000100000000000108000000001100000056476C6F62616C73333234333035323931000000010000000100010400000000324F040C1900000000000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000
  */
}

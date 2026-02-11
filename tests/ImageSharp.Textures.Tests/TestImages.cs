// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Textures.Tests;

/// <summary>
/// Class that contains all the relative test image paths in the Images/Input/Formats directory.
/// </summary>
public static class TestImages
{
    public static class Ktx
    {
        public const string Rgba = "rgba8888.ktx";

        public static class Hdr
        {
            public const string R16 = "hdr-rgb-r16.ktx";
            public const string R32 = "hdr-rgb-r32.ktx";
            public const string Rg16 = "hdr-rgb-rg16.ktx";
            public const string Rg32 = "hdr-rgb-rg32.ktx";
            public const string Rgb16 = "hdr-rgb-rgb16.ktx";
            public const string Rgb32 = "hdr-rgb-rgb32.ktx";
            public const string Rgba16 = "hdr-rgba-rgba16.ktx";
            public const string Rgba32 = "hdr-rgba-rgba32.ktx";
        }
    }

    public static class Ktx2
    {
        public static class Astc
        {
            // Flat textures with various block sizes
            public const string Ldr_4x4_FlightHelmet = "astc_ldr_4x4_FlightHelmet_baseColor.ktx2";
            public const string Ldr_5x4_IronBars = "astc_ldr_5x4_Iron_Bars_001_normal.ktx2";
            public const string Ldr_6x5_FlightHelmet = "astc_ldr_6x5_FlightHelmet_baseColor.ktx2";
            public const string Ldr_6x6_IronBars = "astc_ldr_6x6_Iron_Bars_001_normal.ktx2";
            public const string Ldr_6x6_Posx = "astc_ldr_6x6_posx.ktx2";
            public const string Ldr_8x6_FlightHelmet = "astc_ldr_8x6_FlightHelmet_baseColor.ktx2";
            public const string Ldr_8x8_FlightHelmet = "astc_ldr_8x8_FlightHelmet_baseColor.ktx2";
            public const string Ldr_10x5_FlightHelmet = "astc_ldr_10x5_FlightHelmet_baseColor.ktx2";
            public const string Ldr_12x10_FlightHelmet = "astc_ldr_12x10_FlightHelmet_baseColor.ktx2";
            public const string Ldr_12x12_FlightHelmet = "astc_ldr_12x12_FlightHelmet_baseColor.ktx2";

            // Volume textures
            public const string Ldr_6x6_3dTex = "astc_ldr_6x6_3dtex_7.ktx2";

            // Array textures
            public const string Ldr_6x6_ArrayTex = "astc_ldr_6x6_arraytex_7.ktx2";
            public const string Ldr_6x6_ArrayTex_Mipmap = "astc_ldr_6x6_arraytex_7_mipmap.ktx2";

            // Cubemap textures
            public const string Ldr_Cubemap_6x6 = "astc_ldr_cubemap_6x6.ktx2";

            // Mipmap tests with different quality settings
            public const string Mipmap_Ldr_4x4_Posx = "astc_mipmap_ldr_4x4_posx.ktx2";
            public const string Mipmap_Ldr_6x5_Posx = "astc_mipmap_ldr_6x5_posx.ktx2";
            public const string Mipmap_Ldr_6x6_Fast = "astc_mipmap_ldr_6x6_kodim17_fast.ktx2";
            public const string Mipmap_Ldr_6x6_Medium = "astc_mipmap_ldr_6x6_kodim17_medium.ktx2";
            public const string Mipmap_Ldr_6x6_Fastest = "astc_mipmap_ldr_6x6_kodim17_fastest.ktx2";
            public const string Mipmap_Ldr_6x6_Posx = "astc_mipmap_ldr_6x6_posx.ktx2";
            public const string Mipmap_Ldr_6x6_Posy = "astc_mipmap_ldr_6x6_posy.ktx2";
            public const string Mipmap_Ldr_6x6_Posz = "astc_mipmap_ldr_6x6_posz.ktx2";
            public const string Mipmap_Ldr_8x6_Posx = "astc_mipmap_ldr_8x6_posx.ktx2";
            public const string Mipmap_Ldr_8x8_Posx = "astc_mipmap_ldr_8x8_posx.ktx2";
            public const string Mipmap_Ldr_10x5_Posx = "astc_mipmap_ldr_10x5_posx.ktx2";
            public const string Mipmap_Ldr_12x10_Posx = "astc_mipmap_ldr_12x10_posx.ktx2";
            public const string Mipmap_Ldr_12x12_Posx = "astc_mipmap_ldr_12x12_posx.ktx2";
            public const string Mipmap_Ldr_Cubemap_6x6 = "astc_mipmap_ldr_cubemap_6x6.ktx2";
        }

        public static class Hdr
        {
            public const string R16 = "hdr-rgb16-r16.ktx2";
            public const string Rg16 = "hdr-rgb16-rg16.ktx2";
            public const string Rgb16 = "hdr-rgb16-rgb16.ktx2";
            public const string Rgba16 = "hdr-rgb16-rgba16.ktx2";
            public const string R32 = "hdr-rgb32-r32.ktx2";
            public const string Rg32 = "hdr-rgb32-rg32.ktx2";
            public const string Rgb32 = "hdr-rgb32-rgb32.ktx2";
            public const string Rgba32 = "hdr-rgb32-rgba32.ktx2";
            public const string Rgb9e5 = "hdr-rgb32-rgb9e5.ktx2";
            public const string B10g11r11 = "hdr-rgb32-b10g11r11.ktx2";
        }
    }
}

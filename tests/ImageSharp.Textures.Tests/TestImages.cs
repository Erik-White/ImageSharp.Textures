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

        public static class Astc
        {
            public const string Rgb32_8x8 = "astc-rgba32-8x8.ktx";
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
            public const string Rgba32_4x4 = "astc_rgba32_4x4.ktx2";
            public const string Rgba32_5x4 = "astc_rgba32_5x4.ktx2";
            public const string Rgba32_5x5 = "astc_rgba32_5x5.ktx2";
            public const string Rgba32_6x5 = "astc_rgba32_6x5.ktx2";
            public const string Rgba32_6x6 = "astc_rgba32_6x6.ktx2";
            public const string Rgba32_8x5 = "astc_rgba32_8x5.ktx2";
            public const string Rgba32_8x6 = "astc_rgba32_8x6.ktx2";
            public const string Rgba32_8x8 = "astc_rgba32_8x8.ktx2";
            public const string Rgba32_10x5 = "astc_rgba32_10x5.ktx2";
            public const string Rgba32_10x6 = "astc_rgba32_10x6.ktx2";
            public const string Rgba32_10x8 = "astc_rgba32_10x8.ktx2";
            public const string Rgba32_10x10 = "astc_rgba32_10x10.ktx2";
            public const string Rgba32_12x10 = "astc_rgba32_12x10.ktx2";
            public const string Rgba32_12x12 = "astc_rgba32_12x12.ktx2";

            public const string Rgb32_sRgb_4x4 = "valid_ASTC_4x4_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_5x4 = "valid_ASTC_5x4_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_5x5 = "valid_ASTC_5x5_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_6x5 = "valid_ASTC_6x5_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_6x6 = "valid_ASTC_6x6_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_8x5 = "valid_ASTC_8x5_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_8x6 = "valid_ASTC_8x6_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_8x8 = "valid_ASTC_8x8_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_10x5 = "valid_ASTC_10x5_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_10x6 = "valid_ASTC_10x6_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_10x8 = "valid_ASTC_10x8_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_10x10 = "valid_ASTC_10x10_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_12x10 = "valid_ASTC_12x10_SRGB_BLOCK_2D.ktx2";
            public const string Rgb32_sRgb_12x12 = "valid_ASTC_12x12_SRGB_BLOCK_2D.ktx2";

            public const string Rgb32_Unorm_4x4 = "valid_ASTC_4x4_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_5x4 = "valid_ASTC_5x4_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_5x5 = "valid_ASTC_5x5_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_6x5 = "valid_ASTC_6x5_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_6x6 = "valid_ASTC_6x6_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_8x5 = "valid_ASTC_8x5_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_8x6 = "valid_ASTC_8x6_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_8x8 = "valid_ASTC_8x8_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_10x5 = "valid_ASTC_10x5_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_10x6 = "valid_ASTC_10x6_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_10x8 = "valid_ASTC_10x8_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_10x10 = "valid_ASTC_10x10_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_12x10 = "valid_ASTC_12x10_UNORM_BLOCK_2D.ktx2";
            public const string Rgb32_Unorm_12x12 = "valid_ASTC_12x12_UNORM_BLOCK_2D.ktx2";

            // Supercompressed textures (ZLIB)
            public const string Rgb32_Unorm_4x4_Zlib1 = "valid_ASTC_4x4_UNORM_BLOCK_2D_ZLIB_1.ktx2";
            public const string Rgb32_Unorm_4x4_Zlib9 = "valid_ASTC_4x4_UNORM_BLOCK_2D_ZLIB_9.ktx2";

            // Supercompressed textures (ZSTD)
            public const string Rgb32_Unorm_4x4_Zstd1 = "valid_ASTC_4x4_UNORM_BLOCK_2D_ZSTD_1.ktx2";
            public const string Rgb32_Unorm_4x4_Zstd9 = "valid_ASTC_4x4_UNORM_BLOCK_2D_ZSTD_9.ktx2";

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
    }
}

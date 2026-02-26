Decoder for ASTC (Adaptive Scalable Texture Compression) textures, supporting both LDR and HDR content.

Originally developed as the standalone [AstcSharp](https://github.com/Erik-White/AstcSharp) library.

## Features

- Decode ASTC textures to RGBA32 (LDR) or RGBA float (HDR)
- All 2D block footprints from 4x4 to 12x12

## Decoding paths

The decoder employs three block decoding strategies:

1. **Direct decode** — Standard approach for normal blocks using batch unquantization without intermediate allocations
2. **Fused decode** — Accelerated path for single-partition, single-plane LDR blocks with combined decoding and interpolation
3. **Void extent** — Handles constant-color blocks

## Useful links

- [ASTC specification (Khronos Data Format Specification)](https://registry.khronos.org/DataFormat/specs/1.3/dataformat.1.3.html#ASTC)
- [ARM ASTC Encoder](https://github.com/ARM-software/astc-encoder)
- [Google astc-codec](https://github.com/google/astc-codec)

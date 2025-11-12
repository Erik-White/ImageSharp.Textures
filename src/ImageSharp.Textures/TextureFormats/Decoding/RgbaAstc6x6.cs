// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System;

namespace SixLabors.ImageSharp.Textures.TextureFormats.Decoding
{
    /// <summary>
    /// Texture compressed with RgbaAstc6x6.
    /// </summary>
    internal readonly struct RgbaAstc6x6 : IBlock<RgbaAstc6x6>
    {
        public static Size BlockSize => new(6, 6);

        /// <inheritdoc/>
        public int BitsPerPixel => 128 / (BlockSize.Width * BlockSize.Height);

        /// <inheritdoc/>
        public byte PixelDepthBytes => 4;

        /// <inheritdoc/>
        public byte DivSize => 6;

        /// <inheritdoc/>
        public byte CompressedBytesPerBlock => 16;

        /// <inheritdoc/>
        public bool Compressed => true;

        /// <inheritdoc/>
        public Image GetImage(byte[] blockData, int width, int height)
        {
            byte[] decompressedData = this.Decompress(blockData, width, height);
            return Image.LoadPixelData<ImageSharp.PixelFormats.Rgba32>(decompressedData, width, height);
        }

        /// <inheritdoc/>
        public byte[] Decompress(byte[] blockData, int width, int height)
        {
            int blocksWide = (width + BlockSize.Width - 1) / BlockSize.Width;
            int blocksHigh = (height + BlockSize.Height - 1) / BlockSize.Height;
            byte[] decompressedData = new byte[width * height * 4];
            byte[] decodedBlock = new byte[BlockSize.Width * BlockSize.Height * 4];

            int blockIndex = 0;

            for (int by = 0; by < blocksHigh; by++)
            {
                for (int bx = 0; bx < blocksWide; bx++)
                {
                    int blockDataOffset = blockIndex * this.CompressedBytesPerBlock;
                    if (blockDataOffset + this.CompressedBytesPerBlock <= blockData.Length)
                    {
                        AstcDecoder.DecodeBlock(
                            blockData.AsSpan(blockDataOffset, this.CompressedBytesPerBlock),
                            BlockSize.Width,
                            BlockSize.Height,
                            decodedBlock);

                        for (int py = 0; py < BlockSize.Height && ((by * BlockSize.Height) + py) < height; py++)
                        {
                            for (int px = 0; px < BlockSize.Width && ((bx * BlockSize.Width) + px) < width; px++)
                            {
                                int srcIndex = ((py * BlockSize.Width) + px) * 4;
                                int dstX = (bx * BlockSize.Width) + px;
                                int dstY = (by * BlockSize.Height) + py;
                                int dstIndex = ((dstY * width) + dstX) * 4;

                                decompressedData[dstIndex] = decodedBlock[srcIndex];
                                decompressedData[dstIndex + 1] = decodedBlock[srcIndex + 1];
                                decompressedData[dstIndex + 2] = decodedBlock[srcIndex + 2];
                                decompressedData[dstIndex + 3] = decodedBlock[srcIndex + 3];
                            }
                        }
                    }

                    blockIndex++;
                }
            }

            return decompressedData;
        }
    }
}

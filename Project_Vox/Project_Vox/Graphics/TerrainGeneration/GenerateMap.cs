//This class just takes the generation method from the main itteration classes to refactor later.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Devoxelation
{
    public class GenerateMap
    {
        //Map seed
        int seedx;
        Random r = new Random();
        public byte[,][, ,] cubeType = new byte[(int)xychunks, (int)xychunks][, ,];
        static int width = 100;
        static int length = 100;
        static int chunks = 1;
        public static double xychunks = Math.Sqrt((double)chunks);
        BoundingBox[,] thisqube = new BoundingBox[(int)xychunks, (int)xychunks];

        public void Initialize()
        {
            //Make seeds
            seedx = r.Next(60000);
            //Basic map randomization..
            //Arrays for cubes
            cubeType[0, 0] = new byte[width, 128, length];
            //Set all cubes in chunk as air.. "255" the air is skipped in draw.
            for (byte a = 0; a < width; a++)
            {
                for (byte b = 0; b < 128; b++)
                {
                    for (byte c = 0; c < length; c++)
                    {
                        cubeType[0, 0][a, b, c] = 255;
                    }
                }
            }
            for (byte x = 0; x < width; x++)
            {

                for (byte z = 0; z < length; z++)
                {
                    float octave1 = PerlinNoise.noise((x + seedx) * 0.0001f, (z + seedx) * 0.0001f) * 3f;
                    float octave2 = PerlinNoise.noise((x + seedx) * 0.0005f, (z + seedx) * 0.0005f) * 1f;
                    float octave3 = PerlinNoise.noise((x + seedx) * 0.005f, (z + seedx) * 0.005f) * 1f;
                    float octave4 = PerlinNoise.noise((x + seedx) * 0.01f, (z + seedx) * 0.01f) * 2f;
                    float octave5 = PerlinNoise.noise((x + seedx) * 0.03f, (z + seedx) * 0.03f) * 8f;
                    float lowerGroundHeight = octave1 + octave2 + octave3 + octave4 + octave5;
                    if ((60 + (int)lowerGroundHeight) <= 64)
                    {
                        cubeType[0, 0][x, 60 + (int)lowerGroundHeight, z] = 3;
                        if (60 + (int)lowerGroundHeight < 64)
                        {
                            for (int tosea = (60 + (int)lowerGroundHeight); tosea < 65; tosea++)
                            {
                                cubeType[0, 0][x, tosea, z] = 3;
                            }
                        }
                    }
                    else
                        cubeType[0, 0][x, 60 + (int)lowerGroundHeight, z] = 1;
                }
            }
            //Make boudning boxes for the chunk culling. Need working on.
            int minx = 0;
            int minz = 0;
            for (int chunkx = 0; chunkx < xychunks; chunkx++)
            {
                for (int chunkz = 0; chunkz < xychunks; chunkz++)
                {
                    //box around the chunks of cubes
                    minx = chunkx * 16;
                    minz = chunkz * 16;
                    thisqube[chunkx, chunkz] = new BoundingBox(new Vector3(minx, 0, minz), new Vector3((minx + 16), 128, (minz + 16)));
                }
            }
        }
    }
}

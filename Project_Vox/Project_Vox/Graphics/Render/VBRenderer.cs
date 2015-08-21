/* 
 * Edited by Josh Dadak (d005578a) http://www.devoxelation.com
 * As part of Final Year Project at Staffordshire University
 * "Performance of Destructible Game Environments with Voxel Engines"
 * 
 */

/* Original source from 'CubeCrafter'
 * URL: http://cubedefense.codeplex.com/SourceControl/changeset/view/16830
*/

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Devoxelation
{
    /// <summary>
    /// Renders the terrain using a single static vertex buffer.
    /// </summary>
    public class StaticVBRenderer
    {
        private GraphicsDevice graphicsDevice;
        private IndexBuffer cubeIndices;
        private Texture2D cubeTexture;
        private VertexBuffer cubeBuffer;
        private Effect staticVBEffect;

        /// <summary>
        /// Constructor creates and populates the vertex buffers needed for drawing.
        /// </summary>
        /// <param name="graphicsDevice">The game's graphics device</param>
        /// <param name="cubeTexture">The texture for the cubes</param>
        /// <param name="cubePositions">A list of Vector3s that represent the positions of the terrain's cubes</param>
        /// <param name="staticVBEffect">The effect used to draw a static vertex buffer</param>
        public StaticVBRenderer(GraphicsDevice graphicsDevice, Texture2D cubeTexture, List<Vector3> cubePositions, Effect staticVBEffect)
        {
            this.graphicsDevice = graphicsDevice;
            this.staticVBEffect = staticVBEffect;
            this.cubeTexture = cubeTexture;

            //staticVBEffect.CurrentTechnique = staticVBEffect.Techniques["StaticVertexBufferRendering"];
            staticVBEffect.Parameters["Texture"].SetValue(cubeTexture);

            //Use the helper class to generate cube vertices and indices.
            Cube cube = new Cube(graphicsDevice, new Vector3(0, 0, 0), new Vector3(1), cubeTexture);

            VertexPositionNormalTexture[] vertices = cube.Vertices;
            int[] indices = cube.Indices;

            VertexPositionNormalTexture[] bufferVertices = new VertexPositionNormalTexture[cubePositions.Count * vertices.Length];
            int[] bufferIndices = new int[cubePositions.Count * indices.Length];

            //Make copies of the vertex and indice data for each cube position
            for (int cubeIndex = 0; cubeIndex < cubePositions.Count; cubeIndex++)
            {
                for (int vertexIndex = 0; vertexIndex < vertices.Length; vertexIndex++)
                {
                    bufferVertices[cubeIndex * vertices.Length + vertexIndex] = vertices[vertexIndex];
                    bufferVertices[cubeIndex * vertices.Length + vertexIndex].Position += cubePositions[cubeIndex];
                }

                for (int indiceIndex = 0; indiceIndex < indices.Length; indiceIndex++)
                {
                    bufferIndices[cubeIndex * indices.Length + indiceIndex] = indices[indiceIndex] + cubeIndex * vertices.Length;
                }
            }

            cubeBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), bufferVertices.Length, BufferUsage.WriteOnly);
            cubeBuffer.SetData(bufferVertices);

            cubeIndices = new IndexBuffer(graphicsDevice, IndexElementSize.ThirtyTwoBits, bufferIndices.Length, BufferUsage.WriteOnly);
            cubeIndices.SetData(bufferIndices);
        }

        /// <summary>
        /// Renders the terrain using a static vertex buffer.
        /// </summary>
        /// <param name="view">View Matrix</param>
        /// <param name="projection">Projection Matrix</param>
        public void Render(Matrix view, Matrix projection, Texture2D texture)
        {
            graphicsDevice.SetVertexBuffers(new VertexBufferBinding(cubeBuffer, 0, 0));
            graphicsDevice.Indices = cubeIndices;

            staticVBEffect.Parameters["Texture"].SetValue(texture);
            staticVBEffect.Parameters["ViewProjection"].SetValue(Matrix.Multiply(view, projection));

            foreach (EffectPass pass in staticVBEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cubeBuffer.VertexCount, 0, cubeIndices.IndexCount / 3);
            }
        }
    }
}

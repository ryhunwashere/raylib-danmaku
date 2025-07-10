using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;

namespace RaylibDanmaku.Entities.WeaponTypes
{
    ///<summary> Represents a single beam object. Contains function to activate or deactivate the beam. </summary>
    internal class Beam
    {
        public BeamOwner Owner;
        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public NativeColor Color;
        public int TextureId;
        public bool Active;

        // Optional: makes beam always stick to something, like the player or boss
        public Func<Vector2>? FollowTargetFunc;

        /// <summary>
        /// Activate this beam object.
        /// </summary>
        /// <param name="owner">
        ///     BeamOwner.PLAYER if owned by player. <br/>
        ///     BeamOwner.ENEMY if owned by enemy.
        /// </param>
        /// <param name="startPos">Where the beam will start spawning. </param>
        /// <param name="rotation">Rotation in float degrees. </param>
        /// <param name="scale">Change size of beam multiplied by scale. </param>
        /// <param name="color">
        ///     Tint color of the beam. <br/> 
        ///     Set to NativeColor.White to keep the beam texture color as it is. 
        /// </param>
        /// <param name="textureId">Texture ID of the beam (not texture file path). </param>
        /// <param name="followTargetFunc">
        ///     Set to "followTargetFunc: () => target" to make the beam stick to a specific target, like a player or enemy. <br/>
        ///     Set to "followTargetFunc: () => null" to make the beam static based on startPos.
        /// </param>
        public void Activate(
            BeamOwner owner,
            Vector2 startPos,
            float rotation,
            float scale,
            NativeColor color,
            int textureId,
            Func<Vector2>? followTargetFunc = null)
        {
            Owner = owner;
            Position = startPos;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            TextureId = textureId;
            Active = true;
            FollowTargetFunc = followTargetFunc;

            // Console.WriteLine("[Beam.Activate()] Activate called.");
        }

        // Deactivate this beam
        public void Deactivate()
        {
            Active = false;
            FollowTargetFunc = null;
        }
    }
}
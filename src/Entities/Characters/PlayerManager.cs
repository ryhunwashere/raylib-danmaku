using System.Diagnostics;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.PlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters
{
    /// <summary>
    /// Construct a player based on selection, and load weapon textures like bullet and beam.
    /// </summary>
    internal class PlayerManager
    {
        private static Player? player;
        private static BulletManager? bulletManager;
        private static BeamManager? beamManager;

        public static Player? GetPlayer() => player;
        public static BulletManager? GetBulletManager() => bulletManager;
        public static BeamManager? GetBeamManager() => beamManager;

        private static void SelectPlayer(
            float moveSpeed,
            float slowMoveSpeed,
            float hitboxRadius,
            string spritePath,
            bool hasBulletShot,
            bool hasBeamShot)
        {
            bulletManager = new BulletManager();
            beamManager = new BeamManager();

            int bulletTextureId = EngineTexture.LoadTextureFromFile("assets/Bullets/PlayerBullet1.png");
            BulletManager.PlayerBulletTextureId = bulletTextureId;

            int beamTextureId = EngineTexture.LoadTextureFromFile("assets/Beams/PlayerBeam1.png");
            BeamManager.PlayerBeamTextureId = beamTextureId;

            player = new Player(
                moveSpeed,
                slowMoveSpeed,
                hitboxRadius,
                spritePath,
                scale: Config.PLAYER_SCALE
            );

            if (hasBulletShot)
            {
                var bulletShot = new BulletShot(player, bulletManager);
                player.SetBulletShot(bulletShot);
            }
            else
            {
                player.SetBulletShot(null); // explicitly set to null
            }

            if (hasBeamShot)
            {
                var beamShot = new BeamShot(player, beamManager);
                player.SetBeamShot(beamShot);
            }
            else
            {
                player.SetBeamShot(null);
            }
        }

        /// <summary>
        /// Initialize player based on selected player ID.
        /// </summary>
        /// <param name="playerId">Select between player type 1 to 3.</param>
        public static void InitSelectedPlayer(int playerId)
        {
            Trace.Assert(playerId >= 1 && playerId <= 3, "You can only choose between playerId 1 to 3!");
            switch (playerId)
            {
                case 1:
                    SelectPlayer(
                        moveSpeed: 700.0f,
                        slowMoveSpeed: 300.0f,
                        hitboxRadius: 4.0f,
                        spritePath: "assets/Player/player1_sprite.png",
                        hasBulletShot: true,
                        hasBeamShot: false);
                    break;

                case 2:
                    SelectPlayer(
                        moveSpeed: 850.0f,
                        slowMoveSpeed: 200.0f,
                        hitboxRadius: 3.0f,
                        spritePath: "assets/Player/player2_sprite.png",
                        hasBulletShot: false,
                        hasBeamShot: true);
                    break;

                case 3:
                    // TODO: character that can shoot both beam and bullets maybe
                    break;

                default:
                    Trace.TraceWarning("[PlayerManager] Cannot construct player due to invalid playerId.");
                    break;
            }
        }
    }
}
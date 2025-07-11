using System.Diagnostics;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.IPlayerShotTypes;

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

        private static void InitPlayer(
            float moveSpeed,
            float slowMoveSpeed,
            float hitboxRadius,
            string spritePath,
            bool hasBulletShot,
            bool hasBeamShot)
        {
            bulletManager = new BulletManager();
            beamManager = new BeamManager();

            int bulletTextureId = EngineTexture.LoadTexture("assets/Bullets/PlayerBullet1.png");
            BulletManager.PlayerBulletTextureId = bulletTextureId;

            int beamTextureId = EngineTexture.LoadTexture("assets/Beams/PlayerBeam1.png");
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
                BulletShot bulletShot = new(player, bulletManager);
                player.SetBulletShot(bulletShot);
            }
            else
                player.SetBulletShot(null);

            if (hasBeamShot)
            {
                BeamShot beamShot = new(player, beamManager);
                player.SetBeamShot(beamShot);
            }
            else
                player.SetBeamShot(null);
        }

        /// <summary>Initialize player based on selected player ID. </summary>
        /// <param name="playerId"> Select between player type 1 to 3.</param>
        public static void InitSelectedPlayer(int playerId)
        {
            if (playerId < 1 || playerId > 3)
                throw new ArgumentOutOfRangeException(nameof(playerId));

            switch (playerId)
            {
                case 1:
                    InitPlayer(
                        moveSpeed: 700.0f,
                        slowMoveSpeed: 300.0f,
                        hitboxRadius: 4.0f,
                        spritePath: "assets/Player/player1_sprite.png",
                        hasBulletShot: true,
                        hasBeamShot: false);
                    break;
                case 2:
                    InitPlayer(
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
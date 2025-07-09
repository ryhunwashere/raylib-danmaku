using System.Diagnostics;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.PlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters
{
    internal class PlayerManager
    {
        private static Player? player;
        private static BulletManager? bulletManager;
        private static BeamManager? beamManager;

        public static Player? GetPlayer() => player;
        public static BulletManager? GetBulletManager() => bulletManager;
        public static BeamManager? GetBeamManager() => beamManager;

        private enum ShotType { GENERIC_SHOT, BEAM_SHOT };

        private static void SelectPlayer(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, ShotType shotType)
        {
            bulletManager = new BulletManager();
            beamManager = new BeamManager();

            int bulletTextureId = EngineRender.LoadTextureFromFile("assets/Bullets/PlayerBullet1.png");
            BulletManager.PlayerBulletTextureId = bulletTextureId;

            int beamTextureId = EngineRender.LoadTextureFromFile("assets/Beams/PlayerBeam1.png");
            BeamManager.PlayerBeamTextureId = beamTextureId;

            player = new Player(
                moveSpeed,
                slowMoveSpeed,
                hitboxRadius,
                spritePath,
                scale: Config.PLAYER_SCALE
            );

            var genericShot = new GenericShot(player, bulletManager);
            var beamShot = new BeamShot(player, beamManager);

            Trace.Assert(Enum.IsDefined(shotType), $"Invalid ShotType: {shotType}");

            switch (shotType)
            {
                case ShotType.GENERIC_SHOT:
                    player.SetShot(genericShot);
                    break;
                case ShotType.BEAM_SHOT:
                    player.SetShot(beamShot);
                    break;
                default:
                    Trace.TraceWarning("[PlayerManager] Unknown ShotType selected.");
                    break;
            }
        }

        /// <summary>
        /// Init player based on picked player ID.
        /// </summary>
        /// <param name="playerId">Player ID refers to the character number being picked.</param>
        public static void InitSelectedPlayer(int playerId)
        {
            Trace.Assert(playerId >= 1 && playerId <= 2, "You can only choose between playerId 1 or 2!");
            switch (playerId)
            {
                case 1:
                    SelectPlayer(
                        moveSpeed: 700.0f,
                        slowMoveSpeed: 300.0f,
                        hitboxRadius: 4.0f,
                        spritePath: "assets/Player/player1_sprite.png",
                        ShotType.GENERIC_SHOT);
                    break;

                case 2:
                    SelectPlayer(
                        moveSpeed: 850.0f,
                        slowMoveSpeed: 200.0f,
                        hitboxRadius: 3.0f,
                        spritePath: "assets/Player/player2_sprite.png",
                        ShotType.BEAM_SHOT);
                    break;

                default:
                    Trace.TraceWarning("[PlayerManager] Cannot construct player due to invalid playerId.");
                    break;
            }
        }
    }
}
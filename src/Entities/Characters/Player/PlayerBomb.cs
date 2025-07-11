namespace RaylibDanmaku.Entities.Characters
{
    internal class PlayerBomb
    {
        private const float BOMB_COOLDOWN = 3.0f;
        private float bombTimer = BOMB_COOLDOWN;
        private bool CanUse => bombTimer >= BOMB_COOLDOWN;

        public void Update(float dt) => bombTimer += dt;

        public void Use()
        {
            if (CanUse)
            {
                Console.WriteLine("[PlayerBomb] Fire in the hole!");
                bombTimer = 0.0f;
            }
            else
                Console.WriteLine("[PlayerBomb] Bomb is still on cooldown!");
        }

    }
}
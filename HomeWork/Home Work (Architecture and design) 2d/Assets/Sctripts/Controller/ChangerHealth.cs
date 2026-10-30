namespace AsterGame.Controller
{
    internal sealed class ChangerHealth
    {
        private int _maxHealth;


        public ChangerHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
        }


        public void ChangeHealth(ref int currentValueHP, int valueForChang,
            ref bool isAlive)
        {
            currentValueHP += valueForChang;

            if (currentValueHP > _maxHealth)
                currentValueHP = _maxHealth;

            if (currentValueHP <= 0)
                isAlive = false;
        }
    }
}
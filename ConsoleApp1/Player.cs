using System;

namespace Refactoring
{
    #region Player Base Class

    public abstract class Player
    {
        public abstract int Attack();

        public virtual bool CanWearArmor()
        {
            return false;
        }

    }

    #endregion

    #region Derived Classes

        public sealed class Warrior : Player
        {
            public override int Attack()
            {
                return SwingSword();
            }

            public override bool CanWearArmor()
            {
                return true;
            }

            private static int SwingSword()
            {
                var x = new Random(1000);
                return x.Next() % 10;
            }

        }

        public sealed class Mage : Player
        {
            public override int Attack()
            {
                return CastSpell();
            }

            private static int CastSpell()
            {
                return DateTime.Now.Millisecond % 20;
            }
        }

        public sealed class Archer : Player, IArmor
        {
            public override int Attack()
            {
                return ShootArrow();
            }

            public override bool CanWearArmor()
            {
                return IsLight;
            }

            public bool IsLight { get; set; } //TODO: we can also set this property in Archer Consturtor

            private static int ShootArrow()
            {
                return DateTime.Now.Millisecond % 10;
            }
        }

        #endregion

    #region Armor

        public class Armor : IArmor
        {
            public bool IsLight { get; set; }
        }

        public interface IArmor
        {
            bool IsLight { get; set; }
        }

        #endregion
 }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    class Monster
    {
        private int health, quickAttack, strongAttack;
        public Monster(int health, int quickAttack, int strongAttack)
        {
            this.health = health;
            this.quickAttack = quickAttack;
            this.strongAttack = strongAttack;
        }

        public int getHealth()
        {
            return this.health;
        }

        public int getQuickAttack()
        {
            return this.quickAttack;
        }

        public int getStrongAttack()
        {
            return this.strongAttack;
        }

        public void decreaseHealth(int damg)
        {
            this.health = health - damg;
        }

        public void increaseHealth(int heal)
        {
            this.health = health + heal;
        }

    }
}

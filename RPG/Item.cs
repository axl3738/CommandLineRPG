using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace RPG
{
    public abstract class Item
    {
        int quantity;

        public Item() { }
        public Item(int quantity)
        {
            this.quantity = quantity;
        }
        public int GetQuantity() { return this.quantity; }
        public void SetQuantity(int quantity) { this.quantity = quantity; }
    }

    class TitaniteShard : Item
    {
        private int quantity;

        public TitaniteShard(int quantity)
        {
            this.quantity = quantity;
        }

        public string Description()
        {
            return "Used for upgrading armor and weapons up to +4";
        }

        public override string ToString()
        {
            return "Titanite Shard (" + GetQuantity() + ")";
        }
    }

    class LargeTitaniteShard : Item
    {
        private int quantity;

        public LargeTitaniteShard(int quantity)
        {
            this.quantity = quantity;
        }

        public string Description()
        {
            return "Used for upgrading armor and weapons from +5 up to +7";
        }

        public override string ToString()
        {
            return "Large Titanite Shard (" + GetQuantity() + ")";
        }
    }

    class TitaniteChunk : Item
    {
        private int quantity;

        public TitaniteChunk(int quantity)
        {
            this.quantity = quantity;
        }

        public string Description()
        {
            return "Used for upgrading armor and weapons from +7 up to +9";
        }

        public override string ToString()
        {
            return "Titanite Chunk (" + GetQuantity() + ")";
        }
    }

    class TitaniteSlab : Item
    {
        private int quantity;

        public TitaniteSlab(int quantity)
        {
            this.quantity = quantity;
        }

        public string Description()
        {
            return "Used for upgrading armor and weapons from +9 to +10";
        }

        public override string ToString()
        {
            return "Titanite Slab (" + GetQuantity() + ")";
        }
    }
}

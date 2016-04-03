using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryAPI.Domain
{
    public enum ItemType
    {
        Type1,
        Type2,
        Type3,
    }

    public delegate void ExpirationHandler(object sender, EventArgs e);

    public class Item
    {
        public Item(string label, ItemType type, DateTime? expirationDate)
        {
            Label = label;
            Type = type;
            ExpirationDate = expirationDate;
        }

        public int Id { get; internal set; }

        public string Label { get; set; }

        public ItemType Type { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public event ExpirationHandler Expired;

        internal void RaiseExpired()
        {
            if (this.Expired != null)
            {
                this.Expired(this, EventArgs.Empty);
            }
        }
    }
}

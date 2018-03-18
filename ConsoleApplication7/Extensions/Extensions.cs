using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7.Extensions
{
    static class Extensions
    {
        public static void GroupBySuit(this List<Card> cards)
        {
            cards.GroupBy(item => item.suit)
                .Select(group => new { Suits = group.Key, Items = group.ToList() })
                .ToList();
        }
    }
}

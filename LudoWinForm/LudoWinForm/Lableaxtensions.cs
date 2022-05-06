using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoWinForm
{
    public static class Lableaxtensions
    {
        public static Control MoveTo(this Control source, Control target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            source.Location = target.Location;
            return source;
        }
    }
}

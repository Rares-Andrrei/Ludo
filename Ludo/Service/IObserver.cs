using Ludo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.Service
{
    public interface IObserver
    {void Update(Player player);
    }
}

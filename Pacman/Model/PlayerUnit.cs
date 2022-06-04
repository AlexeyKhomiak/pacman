using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class PlayerUnit : BaseGameObject
    {
        double angle;

        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                Notify("Angle");
            }
        }

    }
}

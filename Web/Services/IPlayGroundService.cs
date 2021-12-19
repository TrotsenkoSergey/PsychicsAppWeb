using PlayGroundModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
    public interface IPlayGroundService
    {
        void SetNewPlayGround(int numberOfPsychics);

        PlayGround TryGetPlayGround(out PlayGround playGround);

        void SetNextDesiredValue(int desiredValue);
    }
}

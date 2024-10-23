using System;
using System.Collections.Generic;
using System.Text;

namespace Birds.src.utility
{
    public enum IDs
    {
        #region Entities
        ENTITY_DEFAULT,
        SHOOTER,
        PROJECTILE,
        COMPOSITE,
        CIRCULAR_COMPOSITE,
        SPIKE,
        LINK_COMPOSITE,
        TRIANGULAR_EQUAL_COMPOSITE,
        TRIANGULAR_90ANGLE_COMPOSITE,
        EMPTY_LINK,
        ENGINE,
        #endregion

        #region Utilities
        CLOUD,
        SUN,
        ENTITY_BUTTON,
        #endregion
        
        #region Controllers
        PLAYER,
        CONTROLLER_DEFAULT,
        CHASER_AI,
        CIRCULAR_AI,
        INDECISIVE_AI,
        RANDOM_AI,
        BACKGROUND,
        #endregion

        #region TEAMS
        TEAM_NEUTRAL_HOSTILE,
        TEAM_NEUTRAL_FRIENDLY,
        TEAM_PLAYER,
        TEAM_AI,
        #endregion

        #region Positions
        MIDDLE,
        TOP_RIGHT,
        NOT_BOUND,
        #endregion
    }
}

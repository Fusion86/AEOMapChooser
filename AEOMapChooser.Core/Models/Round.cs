using AEOMapChooser.Core.Enums;

namespace AEOMapChooser.Core.Models
{
    /// <summary>
    /// A round is a collection of multiple matches/maps and possibly a tiebreaker
    /// </summary>
    public class Round
    {
        public Map[] Maps { get; set; }
        public Map Tiebreaker { get; internal set; }

        public bool HasTiebreaker => Tiebreaker != null;
    }
}

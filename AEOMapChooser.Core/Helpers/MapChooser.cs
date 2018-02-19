using AEOMapChooser.Core.Enums;
using AEOMapChooser.Core.Extensions;
using AEOMapChooser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AEOMapChooser.Core.Helpers
{
    public static class MapChooser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapPool"></param>
        /// <param name="numRounds"></param>
        /// <param name="numMatchesPerRound"></param>
        /// <param name="tiebreaker"></param>
        /// <param name="everyMapTypeAtleastOnce"></param>
        /// <returns>Ordered list of rounds</returns>
        public static List<Round> GetRounds(IEnumerable<Map> mapPool, int numRounds, int numMatchesPerRound, bool tiebreaker = false, bool everyMapTypeAtleastOnce = true)
        {
            List<MapType> allTypes = mapPool.Select(x => x.Type).Distinct().ToList();
            List<Round> rounds = new List<Round>();

            for (int roundNr = 0; roundNr < numRounds; roundNr++)
            {
                Round round = new Round();
                Stack<MapType> requiredTypes = null;

                // Create stack with required types in random order
                if (everyMapTypeAtleastOnce)
                    requiredTypes = new Stack<MapType>(allTypes.Shuffle());

                round.Maps = new Map[numMatchesPerRound];

                for (int matchNr = 0; matchNr < numMatchesPerRound; matchNr++)
                {
                    if (everyMapTypeAtleastOnce && requiredTypes.Count > 0)
                    {
                        // No need to check for doubles, since every type exists at most once
                        MapType requiredType = requiredTypes.Pop();
                        round.Maps[matchNr] = mapPool.Where(x => x.Type == requiredType).Random();
                    }
                    else
                    {
                        round.Maps[matchNr] = mapPool
                            .Where(x => !round.Maps.Contains(x)) // Don't include already played maps
                            .Random();
                    }
                }

                if (tiebreaker && numMatchesPerRound % 2 == 0)
                {
                    // TODO: 
                }

                rounds.Add(round);
            }

            return rounds;
        }
    }
}

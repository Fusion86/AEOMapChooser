using AEOMapChooser.Core.Enums;
using AEOMapChooser.Core.Extensions;
using AEOMapChooser.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static Round[] GetRounds(IEnumerable<Map> mapPool, int numRounds, int numMatchesPerRound, bool tiebreaker = false, bool everyMapTypeAtleastOnce = true)
        {
            IReadOnlyList<MapType> allAvailableTypes = mapPool.Select(x => x.Type).Distinct().ToList();
            List<Map> alreadyPlayedMaps = new List<Map>();
            Round[] rounds = new Round[numRounds];

            for (int roundNr = 0; roundNr < numRounds; roundNr++)
            {
                Round round = new Round();
                round.Maps = new Map[numMatchesPerRound];

                // Create a MapType stack in random order
                Stack<MapType> requiredTypes = new Stack<MapType>(allAvailableTypes.RandomMany());

                for (int matchNr = 0; matchNr < numMatchesPerRound; matchNr++)
                {
                    MapType type;
                    if (requiredTypes.Count > 0)
                    {
                        // If we don't have all the required MapTypes in our map pool then we pick the top one off the (randomly ordered) stack
                        type = requiredTypes.Pop();
                    }
                    else if (matchNr > 0 && allAvailableTypes.Count > 1)
                    {
                        // Otherwise we pick a random maptype that is different from the previous round
                        type = allAvailableTypes.Where(x => x != round.Maps[matchNr - 1].Type).Random();
                    }
                    else
                    {
                        // In case there are no required types and no previous maps then we just pick a random type
                        type = allAvailableTypes.Random();
                    }

                    var possibleMaps = mapPool.Where(x => x.Type == type); // Pick right type
                    var alreadyPlayedMapsThisType = alreadyPlayedMaps.Where(x => x.Type == type).ToList();

                    // If we played all maps then remove the first half of the played maps from the ignore list (so that they can be played against)
                    if (alreadyPlayedMapsThisType.Count >= possibleMaps.Count())
                    {
                        for (int i = 0; i < alreadyPlayedMapsThisType.Count / 2 + 1; i++)
                        {
                            alreadyPlayedMaps.Remove(alreadyPlayedMapsThisType[i]);
                        }
                    }

                    // Do NOT use alreadyPlayedMapsThisType here, since that still includese the maps that we deleted in the above code
                    possibleMaps = possibleMaps.Where(x => !alreadyPlayedMaps.Contains(x)); // Remove recently played maps from the possibleMaps list

                    if (possibleMaps.Count() == 0)
                        throw new Exception("No maps available after filtering!");

                    Map map = possibleMaps.Random();
                    round.Maps[matchNr] = map;
                    alreadyPlayedMaps.Add(map);
                }

                // TODO: Tiebreaker

                rounds[roundNr] = round;
            }

            return rounds;
        }
    }
}

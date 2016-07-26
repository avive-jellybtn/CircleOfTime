using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class LevelsModel {

    public class Level {

        public int numOfGreenEnemies;
        public int numOfYellowEnemies;
        public int numOfRedEnemies;
        public int numOfBossEnemies;

        public Level(int numOfGreenEnemies, int numOfYellowEnemies, int numOfRedEnemies, int numOfBossEnemies) {
            this.numOfGreenEnemies = numOfGreenEnemies;
            this.numOfYellowEnemies = numOfYellowEnemies;
            this.numOfRedEnemies = numOfRedEnemies;
            this.numOfBossEnemies = numOfBossEnemies;
        }
    }

    private static SortedDictionary<int, Level> sortedLevelsDict;

    static LevelsModel() {
        InitTrackModel();
    }
    
    private static void InitTrackModel() {
        sortedLevelsDict = new SortedDictionary<int, Level>();
        sortedLevelsDict.Add(1, new Level(10, 0, 0, 0));
        sortedLevelsDict.Add(2, new Level(7, 3,0, 0));
        sortedLevelsDict.Add(3, new Level(5, 3,2, 0));
        sortedLevelsDict.Add(4, new Level(2, 0, 0, 1));
    }

    public static Level GetLevel(int level) {
        return sortedLevelsDict[level];
    }

    public static int HighestLevelIndex() {
        return sortedLevelsDict.Last().Key;
    }

}

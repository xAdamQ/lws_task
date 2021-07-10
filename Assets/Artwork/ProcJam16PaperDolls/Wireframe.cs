/* 
   Procjam 16 paper dolls
   Copyright (C) 2016  Andrew Fray
   
   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using UnityEngine;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

// how to fill the art containers on this hierarchy 
[ExecuteInEditMode]
public abstract class Wireframe : MonoBehaviour {

    // calls PickParts in child
    public void Pick() {
        var newSeed = fixedSeed != -1
            ? fixedSeed
            : (int)DateTime.Now.Ticks;
        Random.InitState(newSeed);
        PickParts();
#if UNITY_EDITOR
        CacheRecentSeed(newSeed);
#endif
    }

    protected abstract void PickParts();

    // helper func to populate a bunch of targets from the same collection,
    // eg all wheels on a car, or two arms on a person.
    // respects the isMirrored state on the collection to force all to have the same sprite.
    protected static void FillContainers(IList<ArtContainer> containers,
                                         ArtCollection collection) {
        var startState = Random.state;
            
        for(int containerIdx = 0; containerIdx < containers.Count; ++containerIdx) {
            if (collection.IsUniversal) {
                Random.state = startState;
            }
            containers[containerIdx].Art = collection.Choose();
        }
    }
    
#if UNITY_EDITOR
    // only valid if you've run Pick() at least once.
    // useful for debugging.
    public IEnumerable<int> RecentSeeds {
        get { return recentSeeds; }
    }
    
    Queue<int> recentSeeds = new Queue<int>();
    
    void CacheRecentSeed(int seed) {
        recentSeeds.Enqueue(seed);
        while(recentSeeds.Count > 3) {
            recentSeeds.Dequeue();
        }
    }
#endif

    //////////////////////////////////////////////////

    [Header("Random")]
    [Tooltip("(Use negative for random seed)")]
    [SerializeField] int fixedSeed = -1;
    [SerializeField] bool isRandomOnEnable = true;

    //////////////////////////////////////////////////

    void OnEnable() {
        if (isRandomOnEnable) {
            Pick();
        }
    }
}

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
using UnityEngine.Assertions;

// a set of sprites, to choose randomly from.
// put a bunch of these in an ArtPicker
public class ArtCollection : ScriptableObject {

    public Sprite[] Art;

    [Tooltip("All art in hierarchy treated with equal chance")]
    public ArtCollection Parent;
    
    [Tooltip("If true, then arrays of ArtContainers will be filled with same sprite. Eg both arms of a person using same sprite")]
    public bool IsUniversal = true;

    [Tooltip("%Chance of no sprite from this collection")]
    [Range(0f,1f)]
    public float EmptyChance = 0f;
    
    [Tooltip("%Chance of mirroring this sprite in X. Could be combined with FlipYChance.")]
    [Range(0f, 1f)]
    public float FlipXChance = 0f;

    [Tooltip("%Chance of mirroring this sprite in Y. Could be combined with FlipXChance.")]
    [Range(0f, 1f)]
    public float FlipYChance = 0f;

    // abstracts parent relationship 

    public int Count {
        get {
            return Art.Length + (Parent == null ? 0 : Parent.Count);
        }
    }

    public Sprite this[int index] {
        get {
            Assert.IsTrue(index >= 0 && index < Count,
                          "unexpected index " + index + " not in range 0<" + Count);
            bool isFromParent = index >= Art.Length;
            return isFromParent ? Parent[index - Art.Length] : Art[index];
        }
    }

    public struct Choice {
        public Sprite Art;
        public bool IsFlippedX;
        public bool IsFlippedY;

        public static readonly Choice s_empty = new Choice { Art = null };
    }

    public Choice Choose() {
        if (EmptyChance > 0f && Random.value <= EmptyChance) {
            return Choice.s_empty;
        }
        Assert.IsNotNull(Art);
        Assert.IsTrue(Count > 0, "expected some objects to choose");
        int selectionIndex = Random.Range(0, Count);
        var art = this[selectionIndex];
        return new Choice {
            Art = art,
            IsFlippedX = FlipXChance > 0f && FlipXChance >= Random.value,
            IsFlippedY = FlipYChance > 0f && FlipYChance >= Random.value,
        };
    }
}

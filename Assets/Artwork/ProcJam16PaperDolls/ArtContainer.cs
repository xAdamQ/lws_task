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

// how to apply a sprite to this object 
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class ArtContainer : MonoBehaviour {

    public ArtCollection.Choice Art {
        set {
            Renderer.sprite = value.Art;

            Renderer.flipX = cachedXFlip != value.IsFlippedX;
            Renderer.flipY = cachedYFlip != value.IsFlippedY;

            if (posJitter != Vector2.zero) {
                transform.localPosition = Jitter(RootPos, posJitter);
            }
            if (scaleJitter != Vector2.zero) {
                transform.localScale = Jitter(RootScale, scaleJitter);
            }
        }
    }

    //////////////////////////////////////////////////

    [Header("Jitter")]
    [Tooltip("+/- both ways")]
    [SerializeField] Vector2 posJitter;
    [Tooltip("+/- both ways")]
    [SerializeField] Vector2 scaleJitter;

    // to work in editor, we have to fetch these lazily 
    SpriteRenderer cachedRenderer;
    static readonly Vector2 s_invalidRoot = new Vector2(float.MaxValue, float.MaxValue);
    Vector2 cachedRootPos = s_invalidRoot;
    Vector2 cachedRootScale = s_invalidRoot;
    bool cachedXFlip = false;
    bool cachedYFlip = false;

    //////////////////////////////////////////////////

    SpriteRenderer Renderer {
        get {
            if (cachedRenderer == null) {
                cachedRenderer = GetComponent<SpriteRenderer>();
                cachedXFlip = cachedRenderer.flipX;
                cachedYFlip = cachedRenderer.flipY;
            }
            Assert.IsNotNull(cachedRenderer, "expected SpriteRenderer on" + gameObject.name);
            return cachedRenderer;
        }
    }

    Vector3 RootPos {
        get {
            if (cachedRootPos == s_invalidRoot) {
                cachedRootPos = transform.localPosition;
            }
            return cachedRootPos;
        }
    }

    Vector3 RootScale {
        get {
            if (cachedRootScale == s_invalidRoot) {
                cachedRootScale = transform.localScale;
            }
            return cachedRootScale;
        }
    }

    static Vector3 Jitter(Vector3 root, Vector3 jitter) {
        // don't jitter in the editor, because we end up accidentally applying it to the prefab
        if (Application.isPlaying) {
            float mag = Random.value * 2 - 1;
            return root + mag * jitter;
        } else {
            // throw away so we're still consistent in edit mode for same seed
#pragma warning disable 0168
            var throwaway = Random.value;
#pragma warning restore 0168
            return root;
        }
    }
}

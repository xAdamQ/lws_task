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

// how to fill the art containers on this hierarchy 
[ExecuteInEditMode]
public class BodyWireframe : Wireframe {

    protected override void PickParts() {
        headContainer.Art = bodyParts.Heads.Choose();
        hairContainer.Art = bodyParts.Hairs.Choose();
        eyesContainer.Art = bodyParts.Eyes.Choose();
        FillContainers(armContainers, bodyParts.Arms);
        FillContainers(legContainers, bodyParts.Legs);
        torsoContainer.Art = bodyParts.Torsos.Choose();
    }

    //////////////////////////////////////////////////

    [Header("Art")]
    [SerializeField] BodyCollections bodyParts;

    [SerializeField] ArtContainer torsoContainer;
    [SerializeField] ArtContainer[] armContainers;
    [SerializeField] ArtContainer eyesContainer;
    [SerializeField] ArtContainer hairContainer;
    [SerializeField] ArtContainer headContainer;
    [SerializeField] ArtContainer[] legContainers;

    ArtContainer cachedTorsoContainer;

    //////////////////////////////////////////////////
}

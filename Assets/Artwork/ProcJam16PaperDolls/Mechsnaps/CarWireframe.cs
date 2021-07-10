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
[RequireComponent(typeof(ArtContainer))]
public class CarWireframe : Wireframe {

    protected override void PickParts() {
        FillContainers(wheelContainers, carParts.Wheels);
        bodyContainer.Art = carParts.Bodies.Choose();
        FillContainers(engineContainers, carParts.Engines);
    }

    //////////////////////////////////////////////////

    [Header("Art")]
    [SerializeField] CarCollections carParts;
    
    [SerializeField] ArtContainer[] wheelContainers;
    [SerializeField] ArtContainer bodyContainer;
    [SerializeField] ArtContainer[] engineContainers;

    //////////////////////////////////////////////////
}

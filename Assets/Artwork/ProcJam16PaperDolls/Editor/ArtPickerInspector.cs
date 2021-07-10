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
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

class WireframeInspector : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        EditorGUILayout.Space();

        if (GUILayout.Button("Refresh")) {
            foreach(var singleTarget in targets) {
                var picker = (Wireframe)singleTarget;
                picker.Pick();
            }
        }

        // display history:
        
        if (targets.Length == 1) {
            var seedHistory = ((Wireframe)target).RecentSeeds;
            if (seedHistory.Any()) {
                EditorGUILayout.LabelField("Recent seeds:");
                ++EditorGUI.indentLevel;
                foreach(var recentSeed in seedHistory) {
                    EditorGUILayout.LabelField(recentSeed.ToString());
                }
                --EditorGUI.indentLevel;
            } else {
                EditorGUILayout.LabelField("(Recent seeds appear here after picking)");
            }
        } else {
            EditorGUILayout.LabelField("(Select exactly one wireframe for seed history)");
        }
    }
}

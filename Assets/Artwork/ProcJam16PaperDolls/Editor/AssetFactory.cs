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
using UnityEditor;
using UnityEngine;
using System.IO;

static class ProjectAssetFactory 
{
    /* add a menu for Foo if Foo derives from ScriptableObject
    [MenuItem("Assets/Create/Foo")]
    public static void CreateFooAsset() {
        CreateAsset<Foo>();
    }
    */

    [MenuItem("Assets/Create/Art Collection")]
    public static void CreateArtCollection() {
        CreateAsset<ArtCollection>();
    }

    [MenuItem("Assets/Create/Body Collections")]
    public static void CreateBodyCollections() {
        CreateAsset<BodyCollections>();
    }

    [MenuItem("Assets/Create/Car Collections")]
    public static void CreateCarCollections() {
        CreateAsset<CarCollections>();
    }
    
    //////////////////////////////////////////////////

    //////////////////////////////////////////////////

    // use MenuItem() to add a <T> to the context menu
    public static void CreateAsset<T>() where T : ScriptableObject {

        // adapted from http://www.jacobpennock.com/Blog/?page_id=715
            
        T newAsset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(
                Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), 
                "");
        }

        string assetPathAndName = 
            AssetDatabase.GenerateUniqueAssetPath(path + "/" + typeof(T) + ".asset");

        AssetDatabase.CreateAsset(newAsset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newAsset;
    }
}

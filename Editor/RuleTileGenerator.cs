using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class RuleTileGenerator
{
    [MenuItem("Assets/Create/Rule Tile from Sprite Sheet", false, 350)]
    private static void CreateRuleTileFromSpriteSheet()
    {
#if TILEMAP_EXTRAS_2_2_2_OR_NEWER
        Sprite[] s = System.Array.ConvertAll(AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath (Selection.activeObject)) as Object[]
                , item => item as Sprite);
        if(s.Length != 121){
            Debug.LogError("Sprite Sheet format is Wrong");
            return;
        }
        RuleTile ruleTile = ScriptableObject.CreateInstance("RuleTile") as RuleTile;
        var ruleList = new [] {
            //Single Tiled Blocks
            (new Sprite[]{s[0],s[1],s[11],s[12]},new List<int>(){
            0,2,0,
            2,0,2,
            0,2,0
            }),
            //Inner Corner Tiles
            (new Sprite[]{s[22]},new List<int>(){
            1,1,1,
            1,0,1,
            1,1,2
            }),
            (new Sprite[]{s[23]},new List<int>(){
            1,1,1,
            1,0,1,
            2,1,1
            }),
            (new Sprite[]{s[33]},new List<int>(){
            1,1,2,
            1,0,1,
            1,1,1
            }),
            (new Sprite[]{s[34]},new List<int>(){
            2,1,1,
            1,0,1,
            1,1,1
            }),
            (new Sprite[]{s[39]},new List<int>(){
            2,1,2,
            1,0,1,
            1,1,1
            }),
            (new Sprite[]{s[69]},new List<int>(){
            2,1,1,
            1,0,1,
            2,1,1
            }),
            (new Sprite[]{s[116]},new List<int>(){
            1,1,1,
            1,0,1,
            2,1,2
            }),
            (new Sprite[]{s[76]},new List<int>(){
            1,1,2,
            1,0,1,
            1,1,2
            }),
            (new Sprite[]{s[35]},new List<int>(){
            1,1,2,
            1,0,1,
            2,1,1
            }),
            (new Sprite[]{s[25]},new List<int>(){
            2,1,1,
            1,0,1,
            1,1,2
            }),
            (new Sprite[]{s[2]},new List<int>(){
            1,1,2,
            1,0,1,
            2,1,2
            }),
            (new Sprite[]{s[13]},new List<int>(){
            2,1,2,
            1,0,1,
            1,1,2
            }),
            (new Sprite[]{s[14]},new List<int>(){
            2,1,2,
            1,0,1,
            2,1,1
            }),
            (new Sprite[]{s[3]},new List<int>(){
            2,1,1,
            1,0,1,
            2,1,2
            }),
            (new Sprite[]{s[24]},new List<int>(){
            2,1,2,
            1,0,1,
            2,1,2
            }),
            //Corners
            (new Sprite[]{s[42],s[43],s[53],s[54]},new List<int>(){//Top Right
            0,2,0,
            1,0,2,
            0,1,0
            }),
            (new Sprite[]{s[36],s[37],s[47],s[48]},new List<int>(){//Top Left
            0,2,0,
            2,0,1,
            0,1,0
            }),
            (new Sprite[]{s[102],s[103],s[113],s[114]},new List<int>(){//Bottom Left
            0,1,0,
            2,0,1,
            0,2,0
            }),
            (new Sprite[]{s[108],s[109],s[119],s[120]},new List<int>(){//Bottom Right
            0,1,0,
            1,0,2,
            0,2,0
            }),
            //Vertical Platform
            (new Sprite[]{s[5],s[6],s[7],s[8]},new List<int>(){//Top
            0,2,0,
            2,0,2,
            0,1,0
            }),
            (new Sprite[]{s[16],s[17],s[18],s[19]},new List<int>(){//Middle
            0,1,0,
            2,0,2,
            0,1,0
            }),
            (new Sprite[]{s[27],s[28],s[29],s[30]},new List<int>(){//Bottom
            0,1,0,
            2,0,2,
            0,2,0
            }),
            //Horizontal Platform
            (new Sprite[]{s[55],s[66],s[77],s[88]},new List<int>(){//Left
            0,2,0,
            2,0,1,
            0,2,0
            }),
            (new Sprite[]{s[56],s[67],s[78],s[89]},new List<int>(){//Middle
            0,2,0,
            1,0,1,
            0,2,0
            }),
            (new Sprite[]{s[57],s[68],s[79],s[90]},new List<int>(){//Right
            0,2,0,
            1,0,2,
            0,2,0
            }),
            //Surfaces
            (new Sprite[]{s[49],s[50],s[51],s[52]},new List<int>(){//Top
            0,2,0,
            1,0,1,
            0,1,0
            }),
            (new Sprite[]{s[59],s[70],s[81],s[92]},new List<int>(){//Left
            0,1,0,
            2,0,1,
            0,1,0
            }),
            (new Sprite[]{s[64],s[75],s[86],s[97]},new List<int>(){//Right
            0,1,0,
            1,0,2,
            0,1,0
            }),
            (new Sprite[]{s[104],s[105],s[106],s[107]},new List<int>(){//Bottom
            0,1,0,
            1,0,1,
            0,2,0
            }),
        };
        foreach(var rule in ruleList){
            RuleTile.TilingRule tilingRule = new RuleTile.TilingRule();
            tilingRule.m_Output = RuleTile.TilingRuleOutput.OutputSprite.Random;

            tilingRule.m_Sprites = rule.Item1;

            var dict = new Dictionary<Vector3Int, int>();
            for(int i = 0;i < 9;i++){
                if(rule.Item2[i] != 0)
                    dict.Add(new Vector3Int(i%3-1,-(i/3-1)),rule.Item2[i]);
            }
            tilingRule.ApplyNeighbors(dict);
            ruleTile.m_TilingRules.Add(tilingRule);
        }

            RuleTile.TilingRule fillerTilingRule = new RuleTile.TilingRule();
            fillerTilingRule.m_Output = RuleTile.TilingRuleOutput.OutputSprite.Random;
            var fillerDict = new Dictionary<Vector3Int, int>();
            fillerDict.Add(new Vector3Int(0,-2),1);
            fillerDict.Add(new Vector3Int(0,-1),1);
            fillerDict.Add(new Vector3Int(0,1),1);
            fillerDict.Add(new Vector3Int(0,2),1);
            fillerDict.Add(new Vector3Int(-2,0),1);
            fillerDict.Add(new Vector3Int(-1,0),1);
            fillerDict.Add(new Vector3Int(1,0),1);
            fillerDict.Add(new Vector3Int(2,0),1);
            fillerTilingRule.ApplyNeighbors(fillerDict);
            fillerTilingRule.m_Sprites = new []{s[72],s[73],s[83],s[84]};
            ruleTile.m_TilingRules.Add(fillerTilingRule);

            RuleTile.TilingRule centralTilingRule = new RuleTile.TilingRule();
            centralTilingRule.m_Output = RuleTile.TilingRuleOutput.OutputSprite.Random;
            var centralDict = new Dictionary<Vector3Int, int>();
            centralDict.Add(new Vector3Int(0,-1),1);
            centralDict.Add(new Vector3Int(0,1),1);
            centralDict.Add(new Vector3Int(-1,0),1);
            centralDict.Add(new Vector3Int(1,0),1);
            centralTilingRule.ApplyNeighbors(centralDict);
            centralTilingRule.m_Sprites = new []{s[60],s[61],s[62],s[63],s[71],s[74],s[82],s[85],s[93],s[94],s[95],s[96]};
            ruleTile.m_TilingRules.Add(centralTilingRule);
        
        AssetDatabase.CreateAsset(ruleTile, Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject))+"/"+Selection.activeObject.name+"RuleTile.asset");
#else
        Debug.LogError("Tilemap Extras are either outdated or doesn't exist");
#endif
    }

    [MenuItem("Assets/Create/Rule Tile from Sprite Sheet", true)]
    private static bool CreateRuleTileFromSpriteSheetValidation()
    {
        var selection = Selection.objects;
        if(selection == null || selection.Length != 1 || !(selection[0] is Texture2D))
            return false;
        
        return true;
    }
}
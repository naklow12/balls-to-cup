using System;
using System.Text.RegularExpressions;
using UnityEngine;

class SVGConverter
{
    const string folderPath = "Levels/";
    static readonly char[] pathSeperators = { 'M','L','Z','H','V','C','S','Q','T','A' };

    public static string getPathString(string vectorName)
    {
        var svg = Resources.Load<TextAsset>(folderPath + vectorName);//Loading SVG file from */Assets/Resources/{folderPath}/
        string pattern = @"path\s+d=\"".*\""\s";//Getting path attribute from SVG
        string pattern2 = @"\"".*\""\s";//Getting path attribute's value.
        string svgPath = Regex.Match(svg.text,pattern).Value;
        svgPath = Regex.Match(svgPath, pattern2).Value;
        svgPath = svgPath.Replace("\"", "");//Deleting unnecessary quotes
        svgPath = putSeperatorPrefix(svgPath); //For keeping first letters in pattern.

        return svgPath;
    }

    public static string[] getCoordinates(string pathString)
    {
        string[] lines = pathString.Split('#');
        return lines;
    }

    private static string putSeperatorPrefix(string str)
    {
        for (int i = 0; i < pathSeperators.Length; i++)
        {
            string seperator = pathSeperators[i].ToString();
            str = str.Replace(seperator, "#" + seperator);
        }
        return str;
    }

}

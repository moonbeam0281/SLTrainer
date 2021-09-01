using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SLTrainerApp.Structures
{

    public static class HashesFunctions
    {
        public static string GameTypeToString(this GameType gt) => $"{gt.game}{gt.type}";

        public static GameType StringToGameType(this string finder, HashesDb db, string secondBox = "")
            => secondBox == "" ? db.Hashes.FirstOrDefault(x => x.prefix == finder): db.Hashes.FirstOrDefault(x => $"{x.game}{x.type}" == $"{finder}{secondBox}"); 
            

        public static IEnumerable<string> GetMatchInfo(this List<TextBox> list, HashesDb db, int row, bool fromDisp = true)
        {
            if (fromDisp)
            {
                for (int i = row + 1; i <= row + 3; i++)
                {
                    if (list[i].Text == "") continue;
                    yield return $"{list[i].Text.StringToGameType(db).prefix};{i}|";
                }
            }
            else
            {
                for (int i = row + 1; i <= row + 6; i += 2)
                {
                    if (list[i].Text == "") continue;
                    yield return $"{list[i].Text.StringToGameType(db, list[i+1].Text).prefix};{i}|";
                }
            }
        }

        public static void GenerateTicket(List<TextBox> display, HashesDb hdb, int count, int level, bool multiHash)
        {
            var tempList = new Dictionary<string, HashList>();

            for (int i = 0; i < count; i++)
            {
                if (multiHash)
                {
                    var gameTypeList = new Dictionary<string, GameType>();
                    for (var j = Funks.GetRandom(0, 3); j < 3; j++)
                    {
                        var randomgt = hdb.GetRandomHash(level);
                        if (!gameTypeList.TryAdd(randomgt.prefix, randomgt)) { j--; }
                    }
                    HashList multiHashList;
                    switch (gameTypeList.Count)
                    {
                        case 1:
                            {
                                multiHashList = new HashList() { first = gameTypeList.ElementAt(0).Value, second = null, third = null };
                                break;
                            }
                        case 2:
                            {
                                multiHashList = new HashList() { first = gameTypeList.ElementAt(0).Value, second = gameTypeList.ElementAt(1).Value, third = null };
                                break;
                            }
                        default:
                        case 3:
                            {
                                multiHashList = new HashList() { first = gameTypeList.ElementAt(0).Value, second = gameTypeList.ElementAt(1).Value, third = gameTypeList.ElementAt(2).Value };
                                break;
                            }
                    }
                    if (!tempList.TryAdd(Funks.GetRandom(1000, 6999).ToString(), multiHashList)) { i--; }

                }
                else
                {
                    var tempHash = hdb.GetRandomHash(level);
                    if (!tempList.TryAdd(Funks.GetRandom(1000, 6999).ToString(), new HashList() { first = tempHash, second = null, third = null })) { i--; }
                }
            }
            int c = 0;
            foreach (var x in tempList)
            {
                display[c].Text = x.Key;
                display[c + 1].Text = x.Value.first.prefix;
                display[c + 2].Text = x.Value.second?.prefix;
                display[c + 3].Text = x.Value.third?.prefix;
                c += 4;
            }
        }

    }

    public struct HashList
    {
        public GameType first;
        public GameType? second;
        public GameType? third;
    }

    public struct GameType
    {
        public string prefix { get; set; }
        public string game { get; set; }
        public string type { get; set; }
        public int level { get; set; }
    }

    public class HashesDb
    {
        public List<GameType> Hashes = new List<GameType>();

        public HashesDb()
        {
            var txtDb = getHashCodes();
            for (var i = 0; i < txtDb.Count; i++)
            {
                var y = txtDb[i].Split(' ');
                Hashes.Add(new GameType { prefix = y[0], game = y[1], type = y[2], level = int.Parse(y[3]) });
            }
        }

        public GameType GetRandomHash(int level)
        {
            var temp = Hashes.Where(x => x.level <= level).ToList();
            return temp[Funks.GetRandom(temp.Count)];
        }

        public static List<string> getHashCodes()
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SLTrainerApp.DataBase.Hashes.txt");
            using var reader = new StreamReader(stream);
            var list = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
                list.Add(line);
            return list;
        }
    }
}

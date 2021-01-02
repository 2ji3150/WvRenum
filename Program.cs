using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Console.Write("Paste the path to excute:");
var input = Console.ReadLine();
if (string.IsNullOrEmpty(input)) return;
DirectoryInfo di = new DirectoryInfo(input);
if (!di.Exists) return;

var wvfis = di.EnumerateFiles("*.wv", SearchOption.AllDirectories).ToArray();
var renumTargets = new List<(FileInfo, string)>();

for (int i = 0; i < wvfis.Length; i++) {
    var fi = wvfis[i];
    string newtrackname = $"{i + 1:D2} {fi.Name[3..]}";
    if (fi.Name == newtrackname) Console.WriteLine(fi.Name);
    else {
        Console.WriteLine($"* {fi.Name} → {newtrackname}");
        renumTargets.Add((fi, newtrackname));
    }
}

if (renumTargets.Count > 0) {
    Console.Write($"\n{renumTargets.Count} wvs need to be renum\n\nPress Y to continue:");
    if (Console.ReadKey().KeyChar == 'Y') {
        foreach (var (fi, newtrackname) in renumTargets) fi.MoveTo(Path.Combine(fi.DirectoryName, newtrackname));
        Console.WriteLine("\n\n...Complete");
    }
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey(true);
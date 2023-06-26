using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeosModLoader;
using HarmonyLib;
using FrooxEngine;
using CodeX;
using System.IO;

namespace Photo2ClipBoard
{
    public class Photo2ClipBoard : NeosMod
    {
        public override string Name => "Photo2ClipBoard";
        public override string Author => "kka429";
        public override string Version => "0.0.1";
        public override string Link => "";


        public override void OnEngineInit()
        {
            var harmony = new Harmony("com.kokoa.PDFImport");
            harmony.PatchAll();
        }


        [HarmonyPatch(typeof(PlatformInterface), "Initialize")]
        private class PlatformInterfacePatch
        {
            static bool Prefix(ref System.Type[] _____connectorTypes)
            {
                Msg("Patching PlatformInterface...");
                var connectorList = _____connectorTypes.ToList();
                connectorList.Add(typeof(Photo2ClipBoardConnector));
                _____connectorTypes = connectorList.ToArray();
                Msg("Patching PlatformInterface Done.");
                return true;
            }
        }

        public class Photo2ClipBoardConnector : IPlatformConnector
        {
            public int Priority => 0;
            public string PlatformName => "Photo2ClipBoard";
            public string Username => null;
            public string PlatformUserId => null;
            public bool IsPlatformNameUnique => false;



            public async Task<bool> Initialize(PlatformInterface platformInterface)
            {
                Msg("Init Photo2ClipBoardConnector!");
                return true;
            }

            public void NotifyOfScreenshot(World world, string file, ScreenshotType type, DateTime time)
            {
                MemoryStream s = new MemoryStream();
                Bitmap2D bitmap2D1 = Bitmap2D.Load(file, false, false);
                bitmap2D1.Save(s, "jpg", 85, true);
                s.Seek(0, SeekOrigin.Begin);
                Image img = Image.FromStream(s);
                Clipboard.SetImage(img);
            }

            public void Update()
            {

            }

            public void SetCurrentStatus(World world, bool isPrivate, int totalWorldCount)
            {

            }

            public void ClearCurrentStatus()
            {

            }

            public void NotifyOfLocalUser(User user)
            {

            }

            public void NotifyOfFile(string file, string name)
            {

            }

            public void Dispose()
            {

            }

        }

    }
}

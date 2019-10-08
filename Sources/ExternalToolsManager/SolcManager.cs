namespace VeriSolRunner.ExternalTools
{
    internal class SolcManager : DownloadedToolManager
    {
        public SolcManager(ToolSourceSettings settings) : base(settings)
        {
        }

        protected override void Install()
        {
            if (OsName == "windows")
            {
                DownloadAndUnZip();
            }
            else if (OsName == "linux")
            {
                DownloadAndCopy();
            }
            else if (OsName == "osx")
            {
                CreateSymbolicLink();
            }
        }
    }
}
namespace UWUVCI_WII_TOOLS
{
    public class WiiISO
    {
        public bool extractTIKTMD(string romPath, string outTikPath, string outTmdPath)
        {
            bool ok = false;
            try
            {
                byte[] tmd = new byte[0x208];
                byte[] tik = new byte[0x2A4];
                FileInfo fi = new FileInfo(romPath);


                using (var fs = new FileStream(romPath,
                                             FileMode.Open,
                                             FileAccess.Read))
                {
                    if (fi.Length == 4699979776)
                    {
                        fs.Seek(0xF800000, SeekOrigin.Begin);
                        fs.Read(tik, 0, 0x2A4);
                        fs.Seek(0xF8002C0, SeekOrigin.Begin);
                        fs.Read(tmd, 0, 0x208);
                    }
                    else
                    {
                        fs.Seek(0x50000, SeekOrigin.Begin);
                        fs.Read(tik, 0, 0x2A4);
                        fs.Seek(0x502C0, SeekOrigin.Begin);
                        fs.Read(tmd, 0, 0x208);
                    }

                    fs.Close();
                    File.WriteAllBytes(outTikPath, tik);
                    File.WriteAllBytes(outTmdPath, tmd);
                    ok = true;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                ok = false;
            }
            return ok;
        }
    }
}
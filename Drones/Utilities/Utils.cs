using System.Collections;
using System.Drawing;

namespace Drones.Utilities
{
    public static class Utils
    {
        public static byte[] LoadImageFromPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return Array.Empty<byte>();
                return File.ReadAllBytes(path) ?? Array.Empty<byte>();
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException("File not found", ex.Message);
            }
        }
    }
}

using System.Windows.Forms;

namespace DiskSearch.Biz
{
    public class Explorer
    {
        public string OpenExplorer()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (!result.Equals(DialogResult.OK) ||
                    string.IsNullOrWhiteSpace(fbd.SelectedPath)) return "";
                var fullPathName = fbd.SelectedPath;
                return fullPathName;
            }
        }
    }
}

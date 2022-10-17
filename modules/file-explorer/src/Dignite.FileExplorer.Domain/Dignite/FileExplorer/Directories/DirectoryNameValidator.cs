using System.Text.RegularExpressions;
using Dignite.FileExplorer.Directories;

namespace Dignite.FileExplorer.Directories;
public static class DirectoryNameValidator
{
    public static void CheckDirectoryName(string name)
    {
        Regex regex = new Regex(DirectoryDescriptorConsts.NameRegularExpression);
        if (!regex.IsMatch(name))
        {
            throw new InvalidDirectoryNameException(name);
        }
    }
}

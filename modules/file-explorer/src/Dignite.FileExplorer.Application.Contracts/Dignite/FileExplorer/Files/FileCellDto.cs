namespace Dignite.FileExplorer.Files;
public class FileCellDto
{
    public FileCellDto()
    {
    }

    public FileCellDto(string name, string displayName)
    {
        Name = name;
        DisplayName = displayName;
    }

    public string Name { get; set; }

    public string DisplayName { get; set; }
}

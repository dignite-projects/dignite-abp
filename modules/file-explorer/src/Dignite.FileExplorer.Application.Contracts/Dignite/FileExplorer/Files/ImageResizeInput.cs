namespace Dignite.FileExplorer.Files;
public class ImageResizeInput
{
    public ImageResizeInput()
    {
    }

    public ImageResizeInput(int? width, int? height)
    {
        Width = width;
        Height = height;
    }

    public int? Width { get; set; }

    public int? Height { get; set; }
}

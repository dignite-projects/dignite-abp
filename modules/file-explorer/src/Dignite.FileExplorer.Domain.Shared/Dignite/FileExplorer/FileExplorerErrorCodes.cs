namespace Dignite.FileExplorer;

public static class FileExplorerErrorCodes
{
    public static class Directories
    {
        public const string DirectoryAlreadyExist = "FileExplorer.Directory:0001";
        public const string DirectoryNotExist = "FileExplorer.Directory:0002";
        public const string InvalidDirectoryName = "FileExplorer.Directory:0003";
        public const string InvalidMove = "FileExplorer.Directory:0004";
        public const string ForbidMovingToChild = "FileExplorer.Directory:0005";
    }
    public static class Files
    {
        public const string CellNameNotApplicable = "FileExplorer.File:0001";
        public const string CellNameNotFound = "FileExplorer.File:0002";
        public const string ImageSizeTooSmall = "FileExplorer.File:0003";
        public const string ImageResizeFailure = "FileExplorer.File:0004";
        public const string ImageCompressionFailure = "FileExplorer.File:0005";
    }
}
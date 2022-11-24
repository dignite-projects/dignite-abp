namespace Dignite.FileExplorer;

public static class FileExplorerErrorCodes
{
    public static class Directories
    {
        public const string DirectoryAlreadyExist = "FileExplorer:Directory:0001";
        public const string DirectoryNotExist = "FileExplorer:Directory:0002";
        public const string InvalidDirectoryName = "FileExplorer:Directory:0003";
        public const string InvalidMove = "FileExplorer:Directory:0004";
        public const string ForbidMovingToChild = "FileExplorer:Directory:0005";
    }
}
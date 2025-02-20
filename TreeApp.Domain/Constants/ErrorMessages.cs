namespace TreeApp.Domain.Constants;
public static class ErrorMessages
{
    public const string RootNodeNotFound = "Root node not found";
    public const string TreeNotFound = "Tree not found";
    public const string NodeNotFound = "Node not found";
    public const string ParentNodeNotFound = "Parent node not found";
    public const string ParentNodeDifferentTree = "Parent node belongs to different tree";
    public const string DuplicateNodeName = "Node name must be unique among siblings";
    public const string HasChildren = "You have to delete all children nodes first";
    public const string JournalEntryNotFound = "Journal entry not found";
}
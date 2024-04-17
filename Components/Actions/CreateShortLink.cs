namespace Short.IO.Components.Actions;

[CustomAction(KindConst)]
public class CreateShortLink(
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0
) : BotCustomComponentAction<CreateShortLink>(KindConst, sourceFilePath, sourceLineNumber)
{
    public const string KindConst = $"{Constants.Namespace}.{nameof(CreateShortLink)}";
}

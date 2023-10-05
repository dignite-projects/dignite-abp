using System.Collections.Generic;
namespace Dignite.Abp.AspNetCore.Components.CkEditor;

public class CkEditorOptions : Dictionary<string, object>
{
    public static CkEditorOptions Default
    {
        get
        {
            var options = new CkEditorOptions();
            options["language"] = "zh-cn";


            return options;
        }
    }
}

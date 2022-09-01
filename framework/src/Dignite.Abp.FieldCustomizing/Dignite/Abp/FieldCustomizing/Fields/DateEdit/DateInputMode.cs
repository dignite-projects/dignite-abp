namespace Dignite.Abp.FieldCustomizing.Fields.DateEdit
{
    public enum DateInputMode
    {
        //
        // 摘要:
        //     Only date is allowed to be entered.
        Date = 0,
        //
        // 摘要:
        //     Both date and time are allowed to be entered.
        DateTime = 1,
        //
        // 摘要:
        //     Allowed to select only year and month. Note that not all browser supports this
        //     mode, see https://caniuse.com/input-datetime for more info.
        Month = 2
    }
}

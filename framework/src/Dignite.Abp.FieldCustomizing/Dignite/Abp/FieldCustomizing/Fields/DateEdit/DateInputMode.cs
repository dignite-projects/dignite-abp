namespace Dignite.Abp.FieldCustomizing.Fields.DateEdit;

public enum DateInputMode
{
    /// <summary>
    /// Only date is allowed to be entered.
    /// </summary>
    Date = 0,

    /// <summary>
    /// Both date and time are allowed to be entered.
    /// </summary>
    DateTime = 1,

    /// <summary>
    /// Allowed to select only year and month. 
    /// </summary>
    Month = 2
}

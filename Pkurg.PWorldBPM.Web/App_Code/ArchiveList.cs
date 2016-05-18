using System;

public class ArchiveList
{
    public string Source {get;set;}
    public string userCode { get; set; }
    public string UserName { get; set; }
    public string procID { get; set; }
    public string ProcName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string CreatorName { get; set; }
    private string CreatorDeptName { get; set; }
}

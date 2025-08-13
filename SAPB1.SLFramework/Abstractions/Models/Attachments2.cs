using SAPB1.SLFramework.Enums;

namespace SAPB1.SLFramework.Abstractions.Models
{
    public class Attachments2
    {
        public int AbsoluteEntry { get; set; }
        public IEnumerable<AttachmentLine> Attachments2_Lines { get; set; } = [];
    }

    public class AttachmentLine
    {
        public int? AbsoluteEntry { get; set; }
        public int? LineNum { get; set; }
        public string? SourcePath { get; set; }
        public string? FileName { get; set; }
        public string? FileExtension { get; set; }
        public DateTime? AttachmentDate { get; set; }
        public BoYesNoEnum? Override { get; set; }
        public BoYesNoEnum? CopyToTargetDoc { get; set; }
        public BoYesNoEnum? UserID { get; set; }
        public string? FreeText { get; set; }
    }
}
